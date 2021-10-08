using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AccountCalculator.Domain;
using NUnit.Framework;

namespace AccountCalculatorTests
{
    public class ProgramTests
    {
        [Test]
        public async Task EndToEndTest()
        {
            var testDir = TestContext.CurrentContext.TestDirectory;
            var purchasesFile =
                Path.Combine(
                    testDir,
                    "..",
                    "..",
                    "..",
                    "..",
                    "PurchasedItems.csv");
            var commonCurrency = Currency.EUR;
            var output = new List<string>();
            var info = new List<string>();
            await AccountCalculator.Program.CalculatePurchases(
                purchasesFile,
                commonCurrency,
                output.Add,
                info.Add);

            Assert.That(output, Is.EqualTo(new []
            {
                "2021-04-03T18:49:06+08:00,Fire Maple Heat Exchanger 1L Foldable Cooking Pot,19.53,EUR",
                "2021-07-05T03:07:51-07:00,2 x Anatolian Puzzles - 3000 Pieces,62.54,EUR",
                "2021-07-07T20:09:49+08:00,Camping Mosquito Travel Net,8.15,EUR",
                "2021-07-22T18:16:22+01:00,Defenders Cat and Dog Scatter Granules 750g,12.85,EUR",
                "2021-08-04T11:28:19+01:00,iDventure - Davy Jones Locker - Escape Room game,35.16,EUR",
                "2021-08-09T09:45:49+01:00,Coffee Direct Kenya Blue Mountain Coffee Beans 908g,23.74,EUR",
                "2021-08-23T01:36:08+01:00,EDZ Merino Wool T-Shirt 200g Mens (XXXL Blue),39.99,EUR",
                "2021-09-04T19:26:55-07:00,Huzzle CAST Cyclone/Difficulty Level 5,15.22,EUR",
                "2021-09-04T20:02:03+01:00,ABBA Voyage (Transparent Orange Vinyl),63.41,EUR",
                "2021-09-08T14:22:45+01:00,Nestle Nido Instant Full Cream Milk Powder 900g,8.81,EUR",
                "2021-09-13T04:14:29+01:00,Retro-Bit Official SEGA Mega Drive 8-Button Wireless Controller,35.24,EUR",
                "2021-09-19T19:14:04+01:00,LUTE SINGLE Mattress Protector,19.96,EUR",
                "2021-09-28T18:25:28+01:00,ASUS ROG Zephyrus Duo G551QR Laptop,3783.40,EUR",
                "2021-09-29T08:29:12+01:00,Beschoi Laptop Backpack,64.60,EUR",
                "2021-09-29T08:29:12+01:00,RAINYEAR 15.6 Inch Laptop Sleeve Diamond Foam,14.99,EUR",
                "2021-09-29T13:15:17+01:00,3 Rolls - SPORTTAPE Zinc Oxide Tan Tape,17.61,EUR",
                "2021-09-29T20:34:11+01:00,FLEXTAILGEAR Portable Air Pump,30.53,EUR"
            }));
            
            Assert.That(info, Is.EqualTo(new [] {"Total cost is 4255.73 EUR"}));
        }
    }
}