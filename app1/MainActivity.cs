using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Views;
using Android.Content.Res;
using Android.Support.Design.Internal;
using Firebase.Database;
using Firebase.Database.Query;
using app1.Resources.Models;
using app1.Resources.DbHelper;
using System;
using Android.Media;
using Android.Graphics.Drawables;
using Android.Graphics;
using Android.Support.Design.Resources;
using Android.Util;
using System.Linq;

namespace app1
{
    [Activity(Label = "@string/app_name", Theme = "@style/Theme.NoTitleBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        Button btnLogin;
        ListView listTest;
        TextView txtTeste;
        FireBaseHelper fireBaseHelper = new FireBaseHelper();
        FireBaseProjetosHelper fireBaseProjetosHelper = new FireBaseProjetosHelper();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            /*Window.RequestFeature(WindowFeatures.NoTitle);*/
            Main();
        }
        //private DatabaseReference mDatabase;

        private async void BtnLogin_Click(object sender, System.EventArgs e)
        {
            PaginaLogin();
        }

        private void BtnVoltarLogar_Click(object sender, EventArgs e)
        {
            Main();
        }

        private async void BtnLogar_Click(object sender, EventArgs e)
        {
            EditText email = FindViewById<EditText>(Resource.Id.txtEmailLogin);
            EditText senha = FindViewById<EditText>(Resource.Id.txtSenhaLogin);

            var userEmail = await fireBaseHelper.GetUser(email.Text);
            TextView txtTeste = FindViewById<TextView>(Resource.Id.textLogado);

            if (userEmail != null)
            {
                var userSenha = await fireBaseHelper.GetSenhaExiste(senha.Text);

                if (userSenha != null)
                {
                    PaginaLogado();
                }
                else
                {
                    txtTeste.Text = "Senha ou Email errado!";
                }
            }
            else
            {
                txtTeste.Text = "Senha ou Email errado!";
            }
        }

        private void BtnAddProjeto_Click(object sender, EventArgs e)
        {
            AddProjeto();
        }

        private void BtnVoltaAddProjeto_Click(object sender, EventArgs e)
        {
            PaginaLogado();
        }

        private void BtnVoltarLogado_Click(object sender, EventArgs e)
        {
            Main();

        }

        private void BtnAddUser_Click(object sender, EventArgs e)
        {
            AddUser();
        }
        private void BtnVoltarAdUsuario_Click(object sender, EventArgs e)
        {
            PaginaLogado();
        }

        private void BtnCadastrarUser_Click(object sender, EventArgs e)
        {
            EditText nome = FindViewById<EditText>(Resource.Id.txtNomeUser);
            EditText sobrenome = FindViewById<EditText>(Resource.Id.txtSobrenomeUser);
            EditText email = FindViewById<EditText>(Resource.Id.txtEmailUser);
            EditText senha = FindViewById<EditText>(Resource.Id.txtSenhaUser);

            fireBaseHelper.AddUser(email.Text, senha.Text, nome.Text, sobrenome.Text);

            TextView txtConfirmarCadastroUser = FindViewById<TextView>(Resource.Id.txtConfirmarCadastroUser);

            txtConfirmarCadastroUser.Text = "Usuário admin criado com sucesso";

            nome.Text = "";
            sobrenome.Text = "";
            email.Text = "";
            senha.Text = "";
        }

        private async void BtnCadastrarProjeto_Click(object sender, EventArgs e)
        {
            TextView lblProjetosConfir = FindViewById<TextView>(Resource.Id.lblProjetosConfir);
            EditText txtNome = FindViewById<EditText>(Resource.Id.txtNomeProjeto);
            EditText txtDataInicio = FindViewById<EditText>(Resource.Id.txtDataInicio);
            EditText txtDataFinal = FindViewById<EditText>(Resource.Id.txtDataFinal);
            EditText txtOrganizadores = FindViewById<EditText>(Resource.Id.txtOrganizadores);
            EditText txtDescricao = FindViewById<EditText>(Resource.Id.txtDescricao);

            var spinner = FindViewById<Spinner>(Resource.Id.spinnerCategoria);

            await fireBaseProjetosHelper.AddProjetos(txtNome.Text,
                txtDataInicio.Text,
                txtDataFinal.Text,
                txtOrganizadores.Text,
                txtDescricao.Text,
                spinner.GetItemAtPosition(spinner.SelectedItemPosition).ToString());

            txtNome.Text = "";
            txtDataInicio.Text = "";
            txtDataFinal.Text = "";
            txtOrganizadores.Text = "";
            txtDescricao.Text = "";
            lblProjetosConfir.Text = "Projeto Cadastrado com sucesso";



        }

        private async void BtnIrEsportes_Click(object sender, EventArgs e)
        {

            Esportes();
        }

        private void BtnVoltarListar_Click(object sender, EventArgs e)
        {
            Main();
        }


        private void BtnIrTransporte_Click(object sender, EventArgs e)
        {
            Transporte();
        }

        private void BtnIrTrabalho_Click(object sender, EventArgs e)
        {
            Trabalho();
        }

        private void BtnIrEducacao_Click(object sender, EventArgs e)
        {
            Educacao();
        }

        /*Inicio Funçoes Paginas*/
        public void Main()
        {
            SetContentView(Resource.Layout.activity_main);
            btnLogin = FindViewById<Button>(Resource.Id.btnLogin);

            btnLogin.Click += BtnLogin_Click;

            Button btnIrEsportes = FindViewById<Button>(Resource.Id.btnIrEsportes);
            Button btnIrEducacao = FindViewById<Button>(Resource.Id.btnIrEducacao);
            Button btnIrTrabalho = FindViewById<Button>(Resource.Id.btnIrTrabalho);
            Button btnIrTransporte = FindViewById<Button>(Resource.Id.btnIrTransporte);
            btnIrEsportes.Click += BtnIrEsportes_Click;

            btnIrEducacao.Click += BtnIrEducacao_Click;

            btnIrTrabalho.Click += BtnIrTrabalho_Click;

            btnIrTransporte.Click += BtnIrTransporte_Click;

        }
        public async void Esportes()
        {
            SetContentView(Resource.Layout.ListarProjeto);
            Button btnVoltarListar = FindViewById<Button>(Resource.Id.btnVoltarListar);
            btnVoltarListar.Click += BtnVoltarListar_Click;
            var allProjetos = await fireBaseProjetosHelper.GetAllProjetos();

            var allProjetosFiltrado = allProjetos.Where(a => a.Categoria == "Esportes");

            foreach (var projetos in allProjetosFiltrado)
            {
                LinearLayout container = new LinearLayout(this);
                container.Orientation = Android.Widget.Orientation.Vertical;
                container.SetBackgroundResource(Resource.Color.myGray);

                TextView nomeProjeto = new TextView(this);
                nomeProjeto.Text = "Nome: " + projetos.Nome;
                nomeProjeto.SetTextColor(Color.Black);
                nomeProjeto.TextSize = 20;
                //nomeProjeto.SetTextAppearance(this, Android.Resource.Attribute.TextAppearanceLarge);

                TextView organizadores = new TextView(this);
                organizadores.Text = "Organizadores: " + projetos.Organizadores;
                //organizadores.SetTextAppearance(this, Android.Resource.Attribute.TextAppearanceSmall);

                container.AddView(nomeProjeto);
                container.AddView(organizadores);


                LinearLayout espaco = new LinearLayout(this);
                espaco.Orientation = Android.Widget.Orientation.Horizontal;
                espaco.SetMinimumHeight(20);
                //espaco.SetBackgroundResource(Color.White);
                espaco.SetBackgroundColor(Color.White);

                LinearLayout containerListaProjetos = FindViewById<LinearLayout>(Resource.Id.containerListaProjetos);
                containerListaProjetos.AddView(espaco);
                containerListaProjetos.AddView(container);
                //nomeProjeto.SetTextAppearance(this,str)
            }
        }

        public async void Transporte()
        {
            SetContentView(Resource.Layout.ListarProjeto);
            Button btnVoltarListar = FindViewById<Button>(Resource.Id.btnVoltarListar);
            btnVoltarListar.Click += BtnVoltarListar_Click;
            var allProjetos = await fireBaseProjetosHelper.GetAllProjetos();

            var allProjetosFiltrado = allProjetos.Where(a => a.Categoria == "Transporte");

            foreach (var projetos in allProjetosFiltrado)
            {
                LinearLayout container = new LinearLayout(this);
                container.Orientation = Android.Widget.Orientation.Vertical;
                container.SetBackgroundResource(Resource.Color.myGray);

                TextView nomeProjeto = new TextView(this);
                nomeProjeto.Text = "Nome: " + projetos.Nome;
                nomeProjeto.SetTextColor(Color.Black);
                nomeProjeto.TextSize = 20;
                //nomeProjeto.SetTextAppearance(this, Android.Resource.Attribute.TextAppearanceLarge);

                TextView organizadores = new TextView(this);
                organizadores.Text = "Organizadores: " + projetos.Organizadores;
                //organizadores.SetTextAppearance(this, Android.Resource.Attribute.TextAppearanceSmall);

                container.AddView(nomeProjeto);
                container.AddView(organizadores);


                LinearLayout espaco = new LinearLayout(this);
                espaco.Orientation = Android.Widget.Orientation.Horizontal;
                espaco.SetMinimumHeight(20);
                //espaco.SetBackgroundResource(Color.White);
                espaco.SetBackgroundColor(Color.White);

                LinearLayout containerListaProjetos = FindViewById<LinearLayout>(Resource.Id.containerListaProjetos);
                containerListaProjetos.AddView(espaco);
                containerListaProjetos.AddView(container);
                //nomeProjeto.SetTextAppearance(this,str)
            }
        }

        public async void Trabalho()
        {
            SetContentView(Resource.Layout.ListarProjeto);
            Button btnVoltarListar = FindViewById<Button>(Resource.Id.btnVoltarListar);
            btnVoltarListar.Click += BtnVoltarListar_Click;
            var allProjetos = await fireBaseProjetosHelper.GetAllProjetos();

            var allProjetosFiltrado = allProjetos.Where(a => a.Categoria == "Trabalho");

            foreach (var projetos in allProjetosFiltrado)
            {
                LinearLayout container = new LinearLayout(this);
                container.Orientation = Android.Widget.Orientation.Vertical;
                container.SetBackgroundResource(Resource.Color.myGray);

                TextView nomeProjeto = new TextView(this);
                nomeProjeto.Text = "Nome: " + projetos.Nome;
                nomeProjeto.SetTextColor(Color.Black);
                nomeProjeto.TextSize = 20;
                //nomeProjeto.SetTextAppearance(this, Android.Resource.Attribute.TextAppearanceLarge);

                TextView organizadores = new TextView(this);
                organizadores.Text = "Organizadores: " + projetos.Organizadores;
                //organizadores.SetTextAppearance(this, Android.Resource.Attribute.TextAppearanceSmall);

                container.AddView(nomeProjeto);
                container.AddView(organizadores);


                LinearLayout espaco = new LinearLayout(this);
                espaco.Orientation = Android.Widget.Orientation.Horizontal;
                espaco.SetMinimumHeight(20);
                //espaco.SetBackgroundResource(Color.White);
                espaco.SetBackgroundColor(Color.White);

                LinearLayout containerListaProjetos = FindViewById<LinearLayout>(Resource.Id.containerListaProjetos);
                containerListaProjetos.AddView(espaco);
                containerListaProjetos.AddView(container);
                //nomeProjeto.SetTextAppearance(this,str)
            }
        }

        public async void Educacao()
        {
            SetContentView(Resource.Layout.ListarProjeto);
            Button btnVoltarListar = FindViewById<Button>(Resource.Id.btnVoltarListar);
            btnVoltarListar.Click += BtnVoltarListar_Click;
            var allProjetos = await fireBaseProjetosHelper.GetAllProjetos();

            var allProjetosFiltrado = allProjetos.Where(a => a.Categoria == "Educação");

            foreach (var projetos in allProjetosFiltrado)
            {
                LinearLayout container = new LinearLayout(this);
                container.Orientation = Android.Widget.Orientation.Vertical;
                container.SetBackgroundResource(Resource.Color.myGray);

                TextView nomeProjeto = new TextView(this);
                nomeProjeto.Text = "Nome: " + projetos.Nome;
                nomeProjeto.SetTextColor(Color.Black);
                nomeProjeto.TextSize = 20;
                //nomeProjeto.SetTextAppearance(this, Android.Resource.Attribute.TextAppearanceLarge);

                TextView organizadores = new TextView(this);
                organizadores.Text = "Organizadores: " + projetos.Organizadores;
                //organizadores.SetTextAppearance(this, Android.Resource.Attribute.TextAppearanceSmall);

                container.AddView(nomeProjeto);
                container.AddView(organizadores);


                LinearLayout espaco = new LinearLayout(this);
                espaco.Orientation = Android.Widget.Orientation.Horizontal;
                espaco.SetMinimumHeight(20);
                //espaco.SetBackgroundResource(Color.White);
                espaco.SetBackgroundColor(Color.White);

                LinearLayout containerListaProjetos = FindViewById<LinearLayout>(Resource.Id.containerListaProjetos);
                containerListaProjetos.AddView(espaco);
                containerListaProjetos.AddView(container);
                //nomeProjeto.SetTextAppearance(this,str)
            }
        }

        public void PaginaLogin()
        {
            SetContentView(Resource.Layout.login);

            Button btnLogar = FindViewById<Button>(Resource.Id.btnLogar);

            btnLogar.Click += BtnLogar_Click;

            Button btnVoltarLogar = FindViewById<Button>(Resource.Id.btnVoltarLogar);

            btnVoltarLogar.Click += BtnVoltarLogar_Click;
        }
        public void PaginaLogado()
        {
            SetContentView(Resource.Layout.logado);
            Button btnAddProjeto = FindViewById<Button>(Resource.Id.btnAddProjeto);
            Button btnVoltarLogado = FindViewById<Button>(Resource.Id.btnVoltarLogado);
            Button btnAddUser = FindViewById<Button>(Resource.Id.btnAddUser);

            btnAddProjeto.Click += BtnAddProjeto_Click;
            btnVoltarLogado.Click += BtnVoltarLogado_Click;
            btnAddUser.Click += BtnAddUser_Click;
        }
        public void AddProjeto()
        {
            SetContentView(Resource.Layout.addProjeto);

            Button btnVoltaAddProjeto = FindViewById<Button>(Resource.Id.btnVoltarAddProjeto);
            Button btnCadastrarProjeto = FindViewById<Button>(Resource.Id.btnAdicionarProjeto);
            btnVoltaAddProjeto.Click += BtnVoltaAddProjeto_Click;

            btnCadastrarProjeto.Click += BtnCadastrarProjeto_Click;


        }
        public void AddUser()
        {
            SetContentView(Resource.Layout.addUser);

            Button btnVoltarAdUsuario = FindViewById<Button>(Resource.Id.btnVoltarAdUsuario);
            Button btnCadastrarUser = FindViewById<Button>(Resource.Id.btnCadastrarUser);

            btnCadastrarUser.Click += BtnCadastrarUser_Click;
            btnVoltarAdUsuario.Click += BtnVoltarAdUsuario_Click;


        }
        /*Fim Funçoes Paginas*/

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}