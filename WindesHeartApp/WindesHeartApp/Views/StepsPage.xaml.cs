﻿using FormsControls.Base;
using Microcharts.Forms;
using System;
using WindesHeartApp.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WindesHeartApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StepsPage : ContentPage, IAnimationPage
    {
        public static Label CurrentStepsLabel;
        public static Label CurrentDayLabel;
        public static Label KilometersLabel;
        public static Label KcalLabel;

        public static Button ToggleRealTimeStepsButton;

        public static Button Day1Button;
        public static Button Day2Button;
        public static Button Day3Button;
        public static Button Day4Button;
        public static Button Day5Button;
        public static Button Day6Button;
        public static Button TodayButton;

        public StepsPage()
        {
            InitializeComponent();
            BuildPage();

        }

        protected override void OnAppearing()
        {
            Globals.StepsViewModel.InitOnAppearing();
        }

        private void BuildPage()
        {
            absoluteLayout = new AbsoluteLayout();

            PageBuilder.BuildPageBasics(absoluteLayout, this);
            PageBuilder.AddHeaderImages(absoluteLayout);

            PageBuilder.AddLabel(absoluteLayout, "Steps", 0.10, 0.10, Globals.LightTextColor, "", 0);
            PageBuilder.AddReturnButton(absoluteLayout, this);

            ImageButton previousBtn = new ImageButton
            {
                Source = "arrow_left.png",
                BackgroundColor = Color.Transparent
            };
            AbsoluteLayout.SetLayoutFlags(previousBtn, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(previousBtn, new Rectangle(0.3, 0.135, 0.1, 0.1));
            previousBtn.SetBinding(Button.CommandProperty, new Binding() { Path = "PreviousDayBinding" });
            absoluteLayout.Children.Add(previousBtn);

            ImageButton nextBtn = new ImageButton
            {
                Source = "arrow_right.png",
                BackgroundColor = Color.Transparent
            };
            AbsoluteLayout.SetLayoutFlags(nextBtn, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(nextBtn, new Rectangle(0.7, 0.135, 0.1, 0.1));
            nextBtn.SetBinding(Button.CommandProperty, new Binding() { Path = "NextDayBinding" });
            absoluteLayout.Children.Add(nextBtn);

            CurrentDayLabel = PageBuilder.AddLabel(absoluteLayout, "Today", 0.5, 0.16, Color.Black, "", 0);
            CurrentDayLabel.FontSize = 15;

            CurrentStepsLabel = PageBuilder.AddLabel(absoluteLayout, "0", 0.5, 0.37, Color.Black, "", 0);
            CurrentStepsLabel.SetBinding(Label.TextProperty, new Binding("StepsLabelText"));
            CurrentStepsLabel.FontSize = 40;

            var label = PageBuilder.AddLabel(absoluteLayout, "STEPS", 0.5, 0.45, Color.Black, "", 0);
            label.FontSize = 20;

            ChartView chart = new ChartView
            {
                Rotation = 180
            };
            chart.SetBinding(ChartView.ChartProperty, "Chart");
            AbsoluteLayout.SetLayoutFlags(chart, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(chart, new Rectangle(0.5, 0.25, 0.60, 0.60));
            absoluteLayout.Children.Add(chart);

            KcalLabel = PageBuilder.AddLabel(absoluteLayout, "0 Kcal", 0.5, 0.65, Color.Black, "", 0);
            KcalLabel.FontSize = 20;

            KilometersLabel = PageBuilder.AddLabel(absoluteLayout, "0 Kilometers", 0.5, 0.73, Color.Black, "", 0);
            KilometersLabel.FontSize = 20;

            AddDayButtons(absoluteLayout);
        }

        private void AddDayButtons(AbsoluteLayout absoluteLayout)
        {
            DateTime today = DateTime.Now;
            Day1Button = PageBuilder.AddButton(absoluteLayout, today.AddDays(-6).DayOfWeek.ToString(), "Day1Binding", 0.05, 0.85f, 0.13, 0.1, 200, 11, AbsoluteLayoutFlags.All, Globals.SecondaryColor);
            Day2Button = PageBuilder.AddButton(absoluteLayout, today.AddDays(-5).DayOfWeek.ToString(), "Day2Binding", 0.20, 0.85f, 0.13, 0.1, 200, 11, AbsoluteLayoutFlags.All, Globals.SecondaryColor);
            Day3Button = PageBuilder.AddButton(absoluteLayout, today.AddDays(-4).DayOfWeek.ToString(), "Day3Binding", 0.35, 0.85f, 0.13, 0.1, 200, 11, AbsoluteLayoutFlags.All, Globals.SecondaryColor);
            Day4Button = PageBuilder.AddButton(absoluteLayout, today.AddDays(-3).DayOfWeek.ToString(), "Day4Binding", 0.50, 0.85f, 0.13, 0.1, 200, 11, AbsoluteLayoutFlags.All, Globals.SecondaryColor);
            Day5Button = PageBuilder.AddButton(absoluteLayout, today.AddDays(-2).DayOfWeek.ToString(), "Day5Binding", 0.65, 0.85f, 0.13, 0.1, 200, 11, AbsoluteLayoutFlags.All, Globals.SecondaryColor);
            Day6Button = PageBuilder.AddButton(absoluteLayout, today.AddDays(-1).DayOfWeek.ToString(), "Day6Binding", 0.80, 0.85f, 0.13, 0.1, 200, 11, AbsoluteLayoutFlags.All, Globals.SecondaryColor);
            TodayButton = PageBuilder.AddButton(absoluteLayout, "TODAY", "TodayBinding", 0.95, 0.85f, 0.13, 0.1, 200, 11, AbsoluteLayoutFlags.All, Globals.SecondaryColor);
            TodayButton.IsEnabled = false;
        }

        public IPageAnimation PageAnimation { get; } = new SlidePageAnimation { Duration = AnimationDuration.Short, Subtype = AnimationSubtype.FromTop };

        public void OnAnimationStarted(bool isPopAnimation)
        {
            // Put your code here but leaving empty works just fine
        }

        public void OnAnimationFinished(bool isPopAnimation)
        {
            // Put your code here but leaving empty works just fine
        }
    }
}