using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows.Input;
using ClientsManager.Core.Models;
using ClientsManager.Core.Services.Contracts;
using ClientsManager.Helpers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ClientsManager.ViewModels
{
    public class MainViewModel : Observable
    {
        private readonly IClienteData _clienteData;

        private ICommand _loadedCommand;
        private ICommand _selectionChangedCommand;
        private RelayCommand<Flyout> _addClientCommand;
        private RelayCommand<Flyout> _addAdressCommand;
        private RelayCommand _postClientCommand;
        private RelayCommand _postAdressCommand;
        private RelayCommand<FlipView> _refreshCommand;
        private RelayCommand<Flyout> _updateClienteCommand;
        private RelayCommand _putClienteCommand;

        private ObservableCollection<Cliente> _clientesCollection;
        private ObservableCollection<Endereco> _currentEnderecosCollection;
        private int _currentClientId;
        private int _currentIndex;
        private double _flyoutWidth;
        private double _flyoutHeight;
        private Border _flyoutBorder;
        private string _clienteName;
        private string _clienteCPF;
        private string _clienteBirthday;
        private string _endLogradouro;
        private string _endBairro;
        private string _endCidade;
        private string _endEstado;
        private bool _postedSuccess = false;

        public MainViewModel(IClienteData clienteData)
        {
            _clienteData = clienteData;
        }

        public ICommand LoadedCommand => _loadedCommand ?? (_loadedCommand = new RelayCommand(OnLoaded));
        public ICommand SelectionChangedCommand => _selectionChangedCommand ?? (_selectionChangedCommand = new RelayCommand<FlipView>(OnSelectionChanged));
        public RelayCommand<Flyout> AddClientCommand => _addClientCommand ?? (_addClientCommand = new RelayCommand<Flyout>(OnAddClient));
        public RelayCommand<Flyout> AddAdressCommand => _addAdressCommand ?? (_addAdressCommand = new RelayCommand<Flyout>(OnAddAdressCommand));
        public RelayCommand PostClientCommand => _postClientCommand ?? (_postClientCommand = new RelayCommand(OnPostClient));
        public RelayCommand PostAdressCommand => _postAdressCommand ?? (_postAdressCommand = new RelayCommand(OnPostAdress));
        public RelayCommand<FlipView> RefreshCOmmand => _refreshCommand ?? (_refreshCommand = new RelayCommand<FlipView>(OnRefresh));
        public RelayCommand<Flyout> UpadateClienteCommand => _updateClienteCommand ?? (_updateClienteCommand = new RelayCommand<Flyout>(OnUpdateCliente));
        public RelayCommand PutClienteCommand => _putClienteCommand ?? (_putClienteCommand = new RelayCommand(OnPutCliente));

        public ObservableCollection<Cliente> ClientesCollection
        { get => _clientesCollection; private set { Set(ref _clientesCollection, value); } }
        public ObservableCollection<Endereco> CurrentEnderecosCollection
        { get => _currentEnderecosCollection; private set { Set(ref _currentEnderecosCollection, value); } }
        public double FlyoutWidth { get => _flyoutWidth; private set { Set(ref _flyoutWidth, value); } }
        public double FlyoutHeight { get => _flyoutHeight; private set { Set(ref _flyoutHeight, value); } }
        public string ClienteName { get => _clienteName; set { Set(ref _clienteName, value); } }
        public string ClienteCPF { get => _clienteCPF; set { Set(ref _clienteCPF, value); } }
        public string ClienteBirthday { get => _clienteBirthday; set { Set(ref _clienteBirthday, value); } }
        public string EndLogradouro { get => _endLogradouro; set { Set(ref _endLogradouro, value); } }
        public string EndBairro { get => _endBairro; set { Set(ref _endBairro, value); } }
        public string EndCidade { get => _endCidade; set { Set(ref _endCidade, value); } }
        public string EndEstado { get => _endEstado; set { Set(ref _endEstado, value); } }
        public bool PostedSuccess { get => _postedSuccess; private set { Set(ref _postedSuccess, value); } }

        public void Initialize(Border border)
        {
            _flyoutBorder = border;
        }

        private async void OnLoaded()
        {
            try
            {
                var clientes = await _clienteData.GetAllClientes();
                ClientesCollection = new ObservableCollection<Cliente>(clientes);
            }
            catch (HttpRequestException)
            {

            }
        }

        private async void OnSelectionChanged(FlipView flip)
        {
            try
            {
                if (flip.SelectedIndex == -1)
                    return;
                
                var currentEnderecos = await _clienteData.GetAllEnderecosFromCliente(ClientesCollection[flip.SelectedIndex].Id);
                CurrentEnderecosCollection = new ObservableCollection<Endereco>(currentEnderecos);
                _currentClientId = ClientesCollection[flip.SelectedIndex].Id;
                _currentIndex = flip.SelectedIndex;
            }
            catch (ArgumentNullException)
            {
                CurrentEnderecosCollection = new ObservableCollection<Endereco>();
                _currentClientId = ClientesCollection[flip.SelectedIndex].Id;
                _currentIndex = flip.SelectedIndex;
            }
        }

        private void OnAddClient(Flyout flyout)
        {
            PostedSuccess = false;
            FlyoutWidth = Window.Current.Bounds.Width;
            FlyoutHeight = Window.Current.Bounds.Height - 170;
            flyout.ShowAt(_flyoutBorder);
        }

        private void OnAddAdressCommand(Flyout flyout)
        {
            PostedSuccess = false;
            FlyoutWidth = Window.Current.Bounds.Width;
            FlyoutHeight = Window.Current.Bounds.Height - 170;
            flyout.ShowAt(_flyoutBorder);
        }

        private async void OnPostClient()
        {
            var cliente = new Cliente()
            {
                Nome = ClienteName,
                CPF = ClienteCPF,
                DataNascimento = ClienteBirthday,
                ClientesEnderecos = null
            };

            if (await _clienteData.PostNewClient(cliente))
            {
                PostedSuccess = true;
            }
        }

        private async void OnPostAdress()
        {
            var endereco = new Endereco()
            {
                Logradouro = EndLogradouro,
                Bairro = EndBairro,
                Cidade = EndCidade,
                Estado = EndEstado,
                ClientesEnderecos = null
            };

            if (await _clienteData.PostNewClientAdress(_currentClientId, endereco))
            {
                PostedSuccess = true;
            }
        }

        private async void OnRefresh(FlipView flipView)
        {
            var newClientes = await _clienteData.GetAllClientes();
            ClientesCollection = new ObservableCollection<Cliente>(newClientes);
            flipView.InvalidateMeasure();
            flipView.UpdateLayout();
        }

        private void OnUpdateCliente(Flyout flyout)
        {
            PostedSuccess = false;

            var currentCliente = ClientesCollection[_currentIndex];

            ClienteName = currentCliente.Nome;
            ClienteCPF = currentCliente.CPF;
            ClienteBirthday = currentCliente.DataNascimento;

            PostedSuccess = false;
            FlyoutWidth = Window.Current.Bounds.Width;
            FlyoutHeight = Window.Current.Bounds.Height - 170;
            flyout.ShowAt(_flyoutBorder);
        }

        private async void OnPutCliente()
        {
            var cliente = new Cliente()
            {
                Id = ClientesCollection[_currentIndex].Id,
                Nome = ClienteName,
                CPF = ClienteCPF,
                DataNascimento = ClienteBirthday,
                ClientesEnderecos = null
            };

            if (await _clienteData.UpdateCliente(cliente))
                PostedSuccess = true;
        }
    }
}
