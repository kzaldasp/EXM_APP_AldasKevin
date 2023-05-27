using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EXM_APP_AldasKevin.Page.API
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VMascota : ContentPage
    {
        public VMascota()
        {
            InitializeComponent();
        }

        private string url = "https://cloudcomputingapi2.azurewebsites.net/api" + "/Mascotas/";
        private Model.Mascota[] Mascotas { get; set; }

        private void BuscarMas(object sender, EventArgs e)
        {
            using (var wc = new WebClient())
            {
                wc.Headers.Add("Content-Type", "application/json");

                if (txtId.Text.ToString() != "")
                {
                    var json = wc.DownloadString(url + txtId.Text.ToString());
                    var mascota = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.Mascota>(json);

                    if (mascota != null)
                    {
                        txtId.Text = mascota.Id;
                        txtNombre.Text = mascota.Nombre;
                        txtEspecie.Text = mascota.Especie;
                        txtRaza.Text = mascota.Raza;
                        txtEdad.Text = mascota.Edad.ToString();
                        txtNroChip.Text = mascota.Nro_Chip;
                    }
                    else
                    {
                        txtNombre.Text = "";
                        txtEspecie.Text = "";
                        txtRaza.Text = "";
                        txtEdad.Text = "";
                        txtNroChip.Text = "";
                    }
                }
                else
                {
                    lblDatos.Text = "Inserte un ID";
                }

            }
        }

        private void insertarMascota(object sender, EventArgs e)
        {
            using (var wc = new WebClient())
            {
                wc.Headers.Add("Content-Type", "application/json");
                var datos = new Model.Mascota
                {
                    Id = txtId.Text,
                    Nombre = txtNombre.Text,
                    Especie = txtEspecie.Text,
                    Edad = int.Parse(txtEdad.Text),
                    Raza = txtRaza.Text,
                    Nro_Chip = txtNroChip.Text,
                };

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(datos);
                wc.UploadString(url, "POST", json);
                lblDatos.Text = "DATOS INSERTADOS CORRECTAMENTE";
                txtNombre.Text = "";
                txtEspecie.Text = "";
                txtRaza.Text = "";
                txtEdad.Text = "";
                txtNroChip.Text = "";
            }
        }

        private void actualizarMascota(object sender, EventArgs e)
        {
            using (var wc = new WebClient())
            {
                wc.Headers.Add("Content-Type", "application/json");
                var datos = new Model.Mascota
                {
                    Id = txtId.Text,
                    Nombre = txtNombre.Text,
                    Especie = txtEspecie.Text,
                    Edad = int.Parse(txtEdad.Text),
                    Raza = txtRaza.Text,
                    Nro_Chip = txtNroChip.Text,
                };

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(datos);
                wc.UploadString(url + txtId.Text, "PUT", json);
                lblDatos.Text = "DATOS ACTUALIZADOS CORRECTAMENTE";
                txtNombre.Text = "";
                txtEspecie.Text = "";
                txtRaza.Text = "";
                txtEdad.Text = "";
                txtNroChip.Text = "";
            }
        }

        private void EliminarMascota(object sender, EventArgs e)
        {
            using (var wc = new WebClient())
            {
                wc.Headers.Add("Content-Type", "application/json");
                wc.UploadString(url + txtId.Text.ToString(), "DELETE", "");
                lblDatos.Text = "DATOS ELIMINADOS CORRECTAMENTE";
                txtNombre.Text = "";
                txtEspecie.Text = "";
                txtRaza.Text = "";
                txtEdad.Text = "";
                txtNroChip.Text = "";
            }
        }

    }
}