using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace app1.Resources.Models
{
    class Projetos
    {
        public string Nome { get; set; }
        public string Data_inicio { get; set; }
        public string Categoria { get; set; }
        public string Data_finalizacao { get; set; }
        public string Organizadores { get; set; }
        public string Descricao { get; set; }
    }
}