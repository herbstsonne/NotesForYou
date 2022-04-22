using System.Linq;
using System.Threading.Tasks;
using FakeItEasy;
using NotesForYou.Core.Login;
using NotesForYou.Core.NewEntries;
using NotesForYou.Core.ShowMessage;
using NotesForYou.Test.Mocks;
using NSubstitute;
using NUnit.Framework;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace NotesForYou.Test
{
    public class ViewModelTests
    {
        private IPlatformServices _platformServicesFake;
        private IDataStore<Note> _dataStore;

        public ViewModelTests(IDataStore<Note> dataStore)
        {
            _dataStore = dataStore;
        }

        [SetUp]
        public void Setup()
        {
            _platformServicesFake = A.Fake<IPlatformServices>();
            Device.PlatformServices = _platformServicesFake;
        }

        [Test]
        public void When_login_selected_go_to_entriespage()
        {
            var _loginViewModel = Substitute.For<LoginViewModel>();
            Assert.IsTrue(_loginViewModel.LoginCommand.CanExecute(null));
        }

        [Test]
        public async Task Check_if_new_entry_is_created()
        {
            var _viewModel = Substitute.For<NewNoteViewModel>();

            _dataStore = new MockDataStore();
            var entriesBefore = await _dataStore.GetEntriesAsync();

            Assert.That(entriesBefore.Count(), Is.EqualTo(6));

            _viewModel.SaveCommand.Execute(null);

            var entriesAfter = await _dataStore.GetEntriesAsync();

            Assert.That(entriesAfter.Count(), Is.EqualTo(7));
        }
    }
}
