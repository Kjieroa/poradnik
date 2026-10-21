using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace poradnik
{
    public partial class MainWindow : Window
    {
        private decimal money = 0;

        private decimal clickIncome = 1;
        private decimal passiveIncome = 0;

        private int clickLevel = 0;
        private int powerLevel = 0;
        private int autoLevel = 0;
        private int businessLevel = 0;

        private decimal clickCost = 25;
        private decimal powerCost = 100;
        private decimal autoCost = 150;
        private decimal businessCost = 300;

        private readonly DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();

            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };

            timer.Tick += PassiveIncomeTick;
            timer.Start();

            UpdateUI();
        }

        private void WorkClick(object sender, RoutedEventArgs e)
        {
            money += clickIncome;
            MessageBlock.Text = $"Zarobiono {clickIncome:0.##} zł.";
            UpdateUI();
        }

        private void PassiveIncomeTick(object sender, EventArgs e)
        {
            money += passiveIncome;
            UpdateUI();
        }

        private void UpgradeClick(object sender, RoutedEventArgs e)
        {
            if (!CanBuy(clickCost))
                return;

            money -= clickCost;
            clickLevel++;

            clickIncome += 1;
            clickCost = Math.Ceiling(clickCost * 1.6m);

            MessageBlock.Text = "Ulepszono zarobek za kliknięcie.";
            UpdateUI();
        }

        private void UpgradePower(object sender, RoutedEventArgs e)
        {
            if (!CanBuy(powerCost))
                return;

            money -= powerCost;
            powerLevel++;

            clickIncome += 5;
            powerCost = Math.Ceiling(powerCost * 1.8m);

            MessageBlock.Text = "Zwiększono wydajność pracy.";
            UpdateUI();
        }

        private void UpgradeAuto(object sender, RoutedEventArgs e)
        {
            if (!CanBuy(autoCost))
                return;

            money -= autoCost;
            autoLevel++;

            passiveIncome += 1;
            autoCost = Math.Ceiling(autoCost * 1.7m);

            MessageBlock.Text = "Uruchomiono automatyczne zarabianie.";
            UpdateUI();
        }

        private void UpgradeBusiness(object sender, RoutedEventArgs e)
        {
            if (!CanBuy(businessCost))
                return;

            money -= businessCost;
            businessLevel++;

            passiveIncome += 5;
            businessCost = Math.Ceiling(businessCost * 1.9m);

            MessageBlock.Text = "Firma generuje większe dochody.";
            UpdateUI();
        }

        private bool CanBuy(decimal cost)
        {
            if (money < cost)
            {
                MessageBlock.Text = "Masz za mało pieniędzy na to ulepszenie.";
                return false;
            }

            return true;
        }

        private void UpdateUI()
        {
            MoneyBlock.Text = $"{money:0.##} zł";
            IncomeBlock.Text = $"{clickIncome:0.##} zł / klik";
            PassiveBlock.Text = $"{passiveIncome:0.##} zł / sek.";

            LevelBlock.Text =
                $"{clickLevel + powerLevel + autoLevel + businessLevel}";

            ClickLevelBlock.Text = $"Poziom: {clickLevel}";
            PowerLevelBlock.Text = $"Poziom: {powerLevel}";
            AutoLevelBlock.Text = $"Poziom: {autoLevel}";
            BusinessLevelBlock.Text = $"Poziom: {businessLevel}";

            ClickUpgradeBtn.Content = $"Kup: {clickCost:0} zł";
            PowerUpgradeBtn.Content = $"Kup: {powerCost:0} zł";
            AutoUpgradeBtn.Content = $"Kup: {autoCost:0} zł";
            BusinessUpgradeBtn.Content = $"Kup: {businessCost:0} zł";

            WorkBtn.Content = $"PRACUJ  +{clickIncome:0.##} zł";

            ClickUpgradeBtn.IsEnabled = money >= clickCost;
            PowerUpgradeBtn.IsEnabled = money >= powerCost;
            AutoUpgradeBtn.IsEnabled = money >= autoCost;
            BusinessUpgradeBtn.IsEnabled = money >= businessCost;
        }

        private void ResetGame(object sender, RoutedEventArgs e)
        {
            money = 0;

            clickIncome = 1;
            passiveIncome = 0;

            clickLevel = 0;
            powerLevel = 0;
            autoLevel = 0;
            businessLevel = 0;

            clickCost = 25;
            powerCost = 100;
            autoCost = 150;
            businessCost = 300;

            MessageBlock.Text = "Rozpoczęto nową grę.";
            UpdateUI();
        }

        protected override void OnClosed(EventArgs e)
        {
            timer.Stop();
            base.OnClosed(e);
        }
    }
}