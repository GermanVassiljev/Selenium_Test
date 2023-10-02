using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Interactions;


class Program
{
    static void Main()
    {
        ChromeOptions options = new ChromeOptions();
        options.AddArgument("--disable-notifications");

        IWebDriver driver = new ChromeDriver(options);

        driver.Navigate().GoToUrl("https://vassiljev21.thkit.ee/");

        Actions actions = new Actions(driver);

        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
        wait.Until(ExpectedConditions.ElementIsVisible(By.TagName("a")));

        IReadOnlyCollection<IWebElement> elements_collection = driver.FindElements(By.TagName("a"));
        List<IWebElement> elements = elements_collection.ToList();

        foreach (IWebElement element in elements)
        {
            try
            {
                string background_color_1 = element.GetCssValue("background-color");

                System.Threading.Thread.Sleep(500);

                actions.MoveToElement(element).Perform();

                string background_color_2 = element.GetCssValue("background-color");

                if (!string.Equals(background_color_1, background_color_2, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("| Success! " + ">>>" + element.Text + "<<<" + " is changed background color |");
                }
                else
                {
                    Console.WriteLine("| Error! " + ">>>" + element.Text + "<<<" + " is not working |");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Work is end");
                driver.Quit();
            }
            
        }
    }
}