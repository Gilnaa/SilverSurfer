﻿using SilverSurferLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Diagnostics;
using SilverSurferLib.Tokens;
using System.Collections.ObjectModel;

namespace SilverSurfer
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class MainPage : SilverSurfer.Common.LayoutAwarePage
    {
        private const string HistoryKey = "History";

        private ViewModel viewModel;
        private Evaluator evaluator;
        public MainPage()
        {
            this.InitializeComponent();
            evaluator = new Evaluator();
            DataContext = (viewModel = new ViewModel());
            RefreshVariables();
        }

        private void RefreshVariables()
        {
            variableList.ItemsSource = evaluator.Variables.Select((k, v) => new { Key = k.Key, Value = k.Value });
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            if (pageState == null)
                return;

            if (pageState.ContainsKey(HistoryKey))
                viewModel.History = pageState[HistoryKey] as ObservableCollection<LogModel>;
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
            pageState[HistoryKey] = viewModel.History;
        }

        private void Evaluate()
        {
            string input = InputEntry.Text.Replace(" ", string.Empty);

            if (string.IsNullOrWhiteSpace(input))
                return;

            Evaluate(Evaluator.Parse(input));
        }
        private void Evaluate(Token e)
        {
            double? result = null;
            string info = string.Empty;
            try
            {
                result = evaluator.Evaluate(e);
                InputEntry.Text = result.ToString();
                InputEntry.SelectionStart = InputEntry.Text.Length;
                InputEntry.SelectionLength = 0;
            }
            catch (EvaluationException ex)
            {
                info = ex.Message;
            }
            catch (Exception)
            {
                info = "Could not evaluate the expression due to an unknown problem";
            }

            viewModel.History.Insert(0,
                new LogModel
                {
                    Expression = e,
                    RawExpression = e.ToString(),
                    Result = result,
                    Info = info,
                    InfoColor = new SolidColorBrush(Windows.UI.Colors.LightGray)
                });
            RefreshVariables();
        }
        private void EvaluateExpression_Click(object sender, RoutedEventArgs e)
        {
            Evaluate();
        }

        private void InputEntry_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
                Evaluate();
            else if (e.Key == VirtualKey.Up)
                InputEntry.Text = viewModel.History[0].RawExpression;
        }
        private void SaveExpressionClick(object sender, RoutedEventArgs e)
        {
            Button b = (sender as Button);
            var expr = (b.Tag as Token);
            viewModel.SavedExpressions.Insert(0, expr);
        }
        private void List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
                return;

            ListBox list = (sender as ListBox);

            var selectedItem = e.AddedItems.First();

            if (selectedItem is Token)
            {
                Evaluate(selectedItem as Token);
            }
            else
            {
                string insertText = (e.AddedItems.First() as dynamic).Key;
                InputEntry.SelectedText = insertText;
                InputEntry.SelectionStart = InputEntry.SelectionStart + InputEntry.SelectionLength;
                InputEntry.SelectionLength = 0;
            }
            InputEntry.Focus(FocusState.Pointer);
            list.SelectedIndex = -1;
        }

        private void ClearData_Click(object sender, RoutedEventArgs e)
        {
            evaluator.Clear();
            DataContext = (viewModel = new ViewModel());
            RefreshVariables();
        }
    }
}
