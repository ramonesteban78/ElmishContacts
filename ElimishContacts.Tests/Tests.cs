using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace ElimishContacts.Tests
{
	[TestFixture(Platform.Android)]
	//[TestFixture(Platform.iOS)]
	public class Tests
	{
		IApp app;
		Platform platform;

		public Tests(Platform platform)
		{
			this.platform = platform;
		}

		[SetUp]
		public void BeforeEachTest()
		{
			app = AppInitializer.StartApp(platform);
		}

        [Test]
        public void AppLaunches()
        {
            app.Repl();
        }


        [Test]
		public void WelcomeTextIsDisplayed()
		{
			AppResult[] results = app.WaitForElement(c => c.Marked("Welcome to Xamarin.Forms!"));
			app.Screenshot("Welcome screen.");

			Assert.IsTrue(results.Any());
		}

        [Test]
        [TestCase("M")]
        [TestCase("toto")]
        [TestCase("titi")]
        public void Tap_AddContact_And_Navigate_Back(string contactName)
        {
            app.WaitForElement(c => c.Marked("Add"));

            app.Tap(c => c.Marked("Add"));

            app.Screenshot("Add contact screen");

            app.WaitForElement(c => c.Marked("Name"));

            // arrange
            app.EnterText("NameEntry", contactName);
            app.DismissKeyboard();
            app.Tap(c => c.Marked("FavoriteSwitch"));

            // act
            app.Tap(c => c.Marked("Save"));

            var resultList = app.WaitForElement(c => c.Marked(contactName));

            app.Screenshot("Contact added");

            // assert
            Assert.IsTrue(resultList.Any());
        }

        [Test]
        [TestCase("M")]
        public void Navigate_EditContact_EditContact(string contactName)
        {
            app.WaitForElement(c => c.Marked("Add"));

            app.Tap(c => c.Marked(contactName));

            app.Screenshot("Edit contact screen");

            app.WaitForElement(c => c.Marked("Name"));

            // arrange
            app.Tap(c => c.Marked("FavoriteSwitch"));

            // act
            app.Tap(c => c.Marked("Save"));

            var resultList = app.WaitForElement(c => c.Marked(contactName));

            app.Screenshot("Contact edited");

            // assert
            Assert.IsTrue(resultList.Any());
        }

        [Test]
        [TestCase("M")]
        public void Navigate_EditContact_DeleteContact(string contactName)
        {
            app.Tap(c => c.Marked(contactName));
            app.WaitForElement(c => c.Marked("Name"));

            app.Screenshot("Edit contact screen");

            app.Tap("DeleteButton");

            app.WaitForElement(c => c.Marked("Add"));

            var resultList = app.Query(c => c.Marked(contactName));

            app.Screenshot("Contact deleted");

            // assert
            Assert.IsFalse(resultList.Any());
        }
	}
}
