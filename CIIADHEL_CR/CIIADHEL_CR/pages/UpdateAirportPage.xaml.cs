﻿using Acr.UserDialogs;
using CIIADHEL_CR.helpers;
using CIIADHEL_CR.models;
using CIIADHEL_CR.services;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;
using Lottie.Forms;
using Xamarin.Forms.Shapes;
using Xamarin.Essentials;
using System.IO;
using System.Reflection;
using System.Text;

namespace CIIADHEL_CR.pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateAirportPage : ContentPage
    {
        public static string _cedula;
        public static string _password;
        public static string _idAirport;
        private int tapCount;
        private bool validarMascaraPista = false;


        public UpdateAirportPage(string cedula, string contrasena, string idAirport)
        {
            InitializeComponent();
            _cedula = cedula;
            _idAirport = idAirport;
            _password = contrasena;
            btnGuardar.Clicked += BtnGuardar_Clicked;
            Login log = new Login(cedula, contrasena, idAirport);
            _idAirport = log.IDAirport();

            var publics = GetPublic();
            pickerPublic.ItemsSource = publics;

            var espacioAereo = GetEspacioAereo();
            pickerEspacioAereo.ItemsSource = espacioAereo;

            var controls = GetControl();
            pickerControl.ItemsSource = controls;
            txtfrecuencia_EMERGENCY.Text = "121.50";

            var superficie = GetSuperficie();
            pickerSuperficie.ItemsSource = superficie;

            var estado1 = GetEstado();
            pickerEstado.ItemsSource = estado1;

            Application.Current.Properties["ultimaPantalla"] = "update";

            //txtcontenido.IsVisible= true;
        }

        protected async override void OnAppearing()
        {

            base.OnAppearing();
            validarMascaraPista = true;
            txtElevación.TextChanged += TxtElevación_TextChanged;
            lottie.PlayAnimation();
            lottie.RepeatMode = RepeatMode.Infinite;
            lottie.Speed = 2.0f;
            stckTodo.IsVisible = false;
            try
            {
                ///validation if textbox are empty
                #region #region "Validations"
                if (string.IsNullOrEmpty(txtNombre_aeropuerto.Text) || txtNombre_aeropuerto.Text == ""
                   || string.IsNullOrEmpty(txtNombre_OACI.Text) || txtNombre_OACI.Text == ""
                   || string.IsNullOrEmpty(txtNombre_ICAO.Text) || txtNombre_ICAO.Text == ""
                   || string.IsNullOrEmpty(txtPistas.Text) || txtPistas.Text == ""
                   || string.IsNullOrEmpty(txtElevación.Text) || txtElevación.Text == ""
                   || string.IsNullOrEmpty(txtASDA_RWAY_1.Text) || txtASDA_RWAY_1.Text == ""
                   || string.IsNullOrEmpty(txtASDA_RWAY_2.Text) || txtASDA_RWAY_2.Text == ""
                   || string.IsNullOrEmpty(txtTODA_RWAY_1.Text) || txtTODA_RWAY_1.Text == ""
                   || string.IsNullOrEmpty(txtTODA_RWAY_2.Text) || txtTODA_RWAY_2.Text == ""
                   || string.IsNullOrEmpty(txtTORA_RWAY_1.Text) || txtTORA_RWAY_1.Text == ""
                   || string.IsNullOrEmpty(txtTORA_RWAY_2.Text) || txtTORA_RWAY_2.Text == ""
                   || string.IsNullOrEmpty(txtLDA_RWAY_1.Text) || txtLDA_RWAY_1.Text == ""
                   || string.IsNullOrEmpty(txtLDA_RWAY_2.Text) || txtLDA_RWAY_2.Text == ""
                   || string.IsNullOrEmpty(txtfrecuencia_TWR.Text) || txtfrecuencia_TWR.Text == ""
                   || string.IsNullOrEmpty(txtfrecuencia_ATIS.Text) || txtfrecuencia_ATIS.Text == ""
                   || string.IsNullOrEmpty(txtfrecuencia_GRND.Text) || txtfrecuencia_GRND.Text == ""
                   || string.IsNullOrEmpty(txtfrecuencia_EMERGENCY.Text) || txtfrecuencia_EMERGENCY.Text == ""
                   || string.IsNullOrEmpty(txtfrecuencia_Otras.Text) || txtfrecuencia_Otras.Text == ""
                   || string.IsNullOrEmpty(txtUbicacion.Text) || txtUbicacion.Text == ""
                   || string.IsNullOrEmpty(txtTelefono1.Text) || txtTelefono1.Text == ""
                   || string.IsNullOrEmpty(txtTelefono2.Text) || txtTelefono2.Text == ""
                   || string.IsNullOrEmpty(txtCoordenadas.Text) || txtCoordenadas.Text == ""
                   || string.IsNullOrEmpty(txtinfo_torre.Text) || txtinfo_torre.Text == ""
                   || string.IsNullOrEmpty(txtinfo_general.Text) || txtinfo_general.Text == ""
                   || string.IsNullOrEmpty(txtNormas_Generales.Text) || txtNormas_Generales.Text == ""
                   || string.IsNullOrEmpty(txtNormas_Particulares.Text) || txtNormas_Particulares.Text == ""
                   || string.IsNullOrEmpty(txtNOTAMS.Text) || txtNOTAMS.Text == "")
                {
                    #endregion

                    await Task.Delay(3000);
                    gridContainer.IsVisible = false;
                    stckTodo.IsVisible = true;
                    Airport_Detail airportId = await AirportServices.getAnAirportById(Convert.ToInt32(_idAirport));

                    txtNombre_aeropuerto.Text = airportId.Aeropuerto.Nombre;
                    txtNombre_OACI.Text = airportId.Aeropuerto.NombreOaci;
                    txtNombre_ICAO.Text = airportId.Aeropuerto.NombreIcao;
                    txtPistas.Text = airportId.Pistas.Pista;
                    txtElevación.Text = airportId.Pistas.Elevacion;
                    int firstSpaceIndex = airportId.Pistas.Elevacion.IndexOf(" ");
                    if (firstSpaceIndex >= 0)
                    {
                        txtElevación.Text = airportId.Pistas.Elevacion.Substring(0, firstSpaceIndex);
                    }


                    txtElevaciónpies.Text = airportId.Pistas.Elevacion;
                    int firstSpaceIndex1 = airportId.Pistas.Elevacion.IndexOf(" ");
                    int lastSpaceIndex = airportId.Pistas.Elevacion.LastIndexOf(" ");
                    if (firstSpaceIndex1 >= 0 && lastSpaceIndex >= 0 && lastSpaceIndex > firstSpaceIndex1)
                    {
                        txtElevaciónpies.Text = airportId.Pistas.Elevacion.Substring(firstSpaceIndex1 + 5, lastSpaceIndex - (firstSpaceIndex1 + 5));
                    }

                    txtASDA_RWAY_1.Text = airportId.Pistas.AsdaRwy1.ToString();
                    txtASDA_RWAY_2.Text = airportId.Pistas.AsdaRwy2.ToString();
                    txtTODA_RWAY_1.Text = airportId.Pistas.TodaRwy1.ToString();
                    txtTODA_RWAY_2.Text = airportId.Pistas.TodaRwy2.ToString();
                    txtTORA_RWAY_1.Text = airportId.Pistas.ToraRwy1.ToString();
                    txtTORA_RWAY_2.Text = airportId.Pistas.ToraRwy2.ToString();
                    txtLDA_RWAY_1.Text = airportId.Pistas.LdaRwy1.ToString();
                    txtLDA_RWAY_2.Text = airportId.Pistas.LdaRwy2.ToString();

                    //storage the list of frequencies and the list of type_frequencies in two array
                    string[] Data_Frec = string.Join("\n", airportId.Frecuencias.Select(c => c.FrecuenciaFrecuencia)).Split(Convert.ToChar("\n"));
                    string[] Data_Frec2 = string.Join("\n", airportId.Frecuencias.Select(c => c.TipoFrecuencia)).Split(Convert.ToChar("\n"));

                    int Frec = Data_Frec.Length;
                    int TipFrec = Data_Frec2.Length;

                    pickerControl.SelectedIndex = (int)airportId.Caracteristicas_Especiales.Controlado;
                    pickerPublic.SelectedIndex = (int)airportId.Caracteristicas_Especiales.Publico;



                    #region "validations list frequencies"//Verification with 5 data from the list frequencies


                    if (Frec <= 6 && TipFrec <= 9)
                    {
                        string[] tiposFrecuencia = { "TWR", "ATIS", "GRND", "EMERGENCY", "Otras" };
                        for (int i = 0; i < Math.Min(Frec, TipFrec); i++)
                        {

                            int tipoIndex = Array.IndexOf(tiposFrecuencia, Data_Frec2[i]);

                            switch (tipoIndex)
                            {
                                case 0:
                                    txtfrecuencia_TWR.Text = Data_Frec[i].ToString();
                                    break;
                                case 1:
                                    txtfrecuencia_ATIS.Text = Data_Frec[i].ToString();
                                    break;
                                case 2:
                                    txtfrecuencia_GRND.Text = Data_Frec[i].ToString();
                                    break;
                                case 3:
                                    txtfrecuencia_EMERGENCY.Text = "121.50";
                                    break;
                                case 4:
                                    txtfrecuencia_Otras.Text = Data_Frec[i].ToString();
                                    break;
                                default:

                                    break;
                            }
                        }
                    }

                    #endregion
                    txtUbicacion.Text = airportId.Contacto.DireccionExacta;
                    string telefono1Value = airportId.Contacto.NumeroTelefono1;
                    if (telefono1Value.ToLower() == "no disponible")
                    {
                        txtTelefono1.Text = "";
                    }
                    else
                    {
                        txtTelefono1.Text = telefono1Value;
                    }
                    txtTelefono2.Text = (string)airportId.Contacto.NumeroTelefono2;

                    string horarioAeropuerto = airportId.Contacto.Horario;
                    if (horarioAeropuerto == "24 horas")
                    {
                        chkHorario.IsChecked = true;
                        txtHoraInicio.IsVisible = false;
                        txtHorafinal.IsVisible = false;
                        LblHinicio.IsVisible = false;
                        LblHfinal.IsVisible = false;
                    }
                    else
                    {
                        chkHorario.IsChecked = false;
                        txtHoraInicio.IsVisible = true;
                        txtHorafinal.IsVisible = true;
                        LblHinicio.IsVisible = true;
                        LblHfinal.IsVisible = true;

                        string[] horas = horarioAeropuerto.Split('-');
                        if (horas.Length == 2)
                        {
                            string primeraHora = horas[0].Trim();
                            string segundaHora = horas[1].Trim();

                            txtHoraInicio.Text = primeraHora;
                            txtHorafinal.Text = segundaHora;
                        }
                    }



                    txtCoordenadas.Text = airportId.Caracteristicas_Especiales.Coordenada.ToString();
                    txtinfo_torre.Text = airportId.Caracteristicas_Especiales.InfoTorre;
                    txtinfo_general.Text = airportId.Caracteristicas_Especiales.InfoGeneral;

                    string espacioAereo = airportId.Caracteristicas_Especiales.EspacioAereo;

                    int ea = pickerEspacioAereo.Items.IndexOf(espacioAereo);
                    if (ea >= 0)
                    {
                        pickerEspacioAereo.SelectedIndex = ea;
                    }

                    string[] combustible = airportId.Caracteristicas_Especiales.Combustible.Split('/');

                    foreach (string value in combustible)
                    {
                        if (value == "Jet Fuel A-1")
                            chkCombustible1.IsChecked = true;
                        else if (value == "AVGAS 80")
                            chkCombustible2.IsChecked = true;
                        else if (value == "AVGAS 100")
                            chkCombustible3.IsChecked = true;
                        else if (value == "AVGAS 100 LL")
                            chkCombustible4.IsChecked = true;
                    }

                    string superficies = airportId.Pistas.SuperficiePista;
                    int s = pickerSuperficie.Items.IndexOf(superficies);
                    if (s >= 0)
                    {
                        pickerSuperficie.SelectedIndex = s;
                    }


                    string estado = airportId.Aeropuerto.EstadoAeropuerto;
                    int e = pickerEstado.Items.IndexOf(estado);
                    if (e >= 0)
                    {
                        pickerEstado.SelectedIndex = e;
                    }
                    txtNormas_Generales.Text = airportId.Caracteristicas_Especiales.NormaGeneral;
                    txtNormas_Particulares.Text = airportId.Caracteristicas_Especiales.NormaParticular;
                    txtNOTAMS.Text = string.Join("\n", airportId.NOTAMS.Select(c => c.NotamNotam));
                    if (airportId.Documento != null)
                    {
                        await Task.Run(() =>
                        {
                            base64File = airportId.Documento.Contenido ?? "";
                            lblPDF.Text = airportId.Documento.nombre_pdf ?? ""; 
                        });
                       
                    }
                    else
                    {
                       
                        base64File = "";
                        lblPDF.Text = "";

                    }
                }

                else
                {
                    UserDialogs.Instance.HideLoading();
                }
            }
            catch (Exception ex)
            {
                await DialogService.ShowErrorAsync("Error", ex.Message, "OK");
            }
        }

        private bool isProcessing = false;
        private void BtnGuardar_Clicked(object sender, EventArgs e)

          {
            if (isProcessing)
                return;


            isProcessing = true;
            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await this.DisplayAlert("Notificación", "¿Realmente Desea guardar los datos?", "Si", "No");
                if (result)
                {
                    try
                    {
                        #region "Validations"


                        if (String.IsNullOrWhiteSpace(txtNombre_aeropuerto.Text))
                        {
                            await DisplayAlert("Advertencia", "El campo Nombre Aeropuerto es obligatorio", "OK");
                        }
                        else if (String.IsNullOrWhiteSpace(txtNombre_OACI.Text))
                        {
                            await DisplayAlert("Advertencia", "El campo Nombre OACI es obligatorio", "OK");
                        }
                        else if (String.IsNullOrWhiteSpace(txtNombre_ICAO.Text))
                        {
                            await DisplayAlert("Advertencia", "El campo Nombre ICAO es obligatorio", "OK");
                        }
                        else if (String.IsNullOrWhiteSpace(txtPistas.Text))
                        {
                            await DisplayAlert("Advertencia", "El campo Pistas es obligatorio", "OK");
                        }

                        else if (String.IsNullOrWhiteSpace(txtPistas.Text))
                        {
                            await DisplayAlert("Advertencia", "El campo Pistas es obligatorio", "OK");
                        }

                        else if (String.IsNullOrWhiteSpace(txtCoordenadas.Text))
                        {
                            await DisplayAlert("Advertencia", "El campo Coordenadas es obligatorio", "OK");
                        }
                        else if (String.IsNullOrWhiteSpace(txtUbicacion.Text))
                        {
                            await DisplayAlert("Advertencia", "El campo Ubicación es obligatorio", "OK");
                        }
                        else if (String.IsNullOrWhiteSpace(txtElevación.Text))
                        {
                            await DisplayAlert("Advertencia", "El campo Elevación es obligatorio", "OK");
                        }
                        else if (String.IsNullOrWhiteSpace(txtASDA_RWAY_1.Text))
                        {
                            await DisplayAlert("Advertencia", "El campo ASDA RWAY 1 es obligatorio", "OK");
                        }
                        else if (String.IsNullOrWhiteSpace(txtASDA_RWAY_2.Text))
                        {
                            await DisplayAlert("Advertencia", "El campo ASDA RWAY 2 es obligatorio", "OK");
                        }
                        else if (String.IsNullOrWhiteSpace(txtTODA_RWAY_1.Text))
                        {
                            await DisplayAlert("Advertencia", "El campo TODA RWAY 1 es obligatorio", "OK");
                        }
                        else if (String.IsNullOrWhiteSpace(txtTODA_RWAY_2.Text))
                        {
                            await DisplayAlert("Advertencia", "El campo TODA RWAY 2 es obligatorio", "OK");
                        }
                        else if (String.IsNullOrWhiteSpace(txtTORA_RWAY_1.Text))
                        {
                            await DisplayAlert("Advertencia", "El campo TORA RWAY 1 es obligatorio", "OK");
                        }
                        else if (String.IsNullOrWhiteSpace(txtTORA_RWAY_2.Text))
                        {
                            await DisplayAlert("Advertencia", "El campo TORA RWAY 2 es obligatorio", "OK");
                        }
                        else if (String.IsNullOrWhiteSpace(txtLDA_RWAY_1.Text))
                        {
                            await DisplayAlert("Advertencia", "El campo LDA RWAY 1 es obligatorio", "OK");
                        }
                        else if (String.IsNullOrWhiteSpace(txtLDA_RWAY_2.Text))
                        {
                            await DisplayAlert("Advertencia", "El campo LDA RWAY 2 es obligatorio", "OK");
                        }
                        else if (String.IsNullOrWhiteSpace(txtinfo_torre.Text))
                        {
                            await DisplayAlert("Advertencia", "El campo Información Torre es obligatorio", "OK");
                        }
                        else if (String.IsNullOrWhiteSpace(txtinfo_general.Text))
                        {
                            await DisplayAlert("Advertencia", "El campo Información General es obligatorio", "OK");
                        }
                        //else if (String.IsNullOrWhiteSpace(txtTelefono1.Text))
                        //{
                        //    await DisplayAlert("Advertencia", "El campo Telefono 1 es obligatorio", "OK");
                        //}
                        else if (String.IsNullOrWhiteSpace(txtTelefono2.Text))
                        {
                            await DisplayAlert("Advertencia", "El campo Telefono 2 es obligatorio", "OK");
                        }
                        else if (String.IsNullOrWhiteSpace(txtNormas_Generales.Text))
                        {
                            await DisplayAlert("Advertencia", "El campo Normas Generales es obligatorio", "OK");
                        }
                        else if (String.IsNullOrWhiteSpace(txtNormas_Particulares.Text))
                        {
                            await DisplayAlert("Advertencia", "El campo Normas Particulares es obligatorio", "OK");
                        }
                        else if (String.IsNullOrWhiteSpace(txtNOTAMS.Text))
                        {
                            await DisplayAlert("Advertencia", "El campo NOTAMS es obligatorio", "OK");
                        }
                        else
                        {
                            #endregion
                            Airport_Detail airportId = await AirportServices.getAnAirportById(Convert.ToInt32(_idAirport));

                            var estadoSeleccionado = pickerEstado.SelectedItem as Airport_Update;
                            var espacioAereoSeleccionado = pickerEspacioAereo.SelectedItem as Airport_Update;
                            var SuperficieSeleccionado = pickerSuperficie.SelectedItem as Airport_Update;
                            string nombreCompleto = lblPDF.Text;
                            if (string.IsNullOrEmpty(base64File))
                            {
                                byte[] noDisponibleBytes = Encoding.UTF8.GetBytes("No disponible");
                                base64File = Convert.ToBase64String(noDisponibleBytes);
                            }
                            if (string.IsNullOrEmpty(nombreCompleto))
                            {
                                nombreCompleto = "No disponible";
                            }
                            var Punto = nombreCompleto.IndexOf('.');
                            int PuntoUltimo = nombreCompleto.LastIndexOf('.');

                            Airport_Update airportX = new Airport_Update
                            {
                                ID_Aeropuerto = Convert.ToInt32(_idAirport),
                                Nombre = txtNombre_aeropuerto.Text,
                                Nombre_OACI = txtNombre_OACI.Text,
                                NombreICAO = txtNombre_ICAO.Text,
                                Usario = _cedula,
                                Espacio_Aereo = espacioAereoSeleccionado.NameEspacioAereo,
                                Estado_Aeropuerto = estadoSeleccionado != null ? estadoSeleccionado.Nameestado : string.Empty,
                                Notam = txtNOTAMS.Text,
                                Publico = pickerPublic.SelectedIndex.ToString(),
                                Controlado = pickerControl.SelectedIndex.ToString(),
                                Info_Torre = txtinfo_torre.Text,
                                Info_General = txtinfo_general.Text,
                                Combustible = Combustibles(chkCombustible1.IsChecked, chkCombustible2.IsChecked, chkCombustible3.IsChecked, chkCombustible4.IsChecked),
                                Norma_General = txtNormas_Generales.Text,
                                Norma_Particular = txtNormas_Particulares.Text,
                                Coordenada = txtCoordenadas.Text,
                                Direccion_Exacta = txtUbicacion.Text,
                                Numero_Telefono1 = txtTelefono1.Text,
                                Numero_Telefono2 = txtTelefono2.Text,
                                Horario = chkHorario.IsChecked ? "24 horas" : $"{txtHoraInicio.Text} - {txtHorafinal.Text}",
                                ATIS = txtfrecuencia_ATIS.Text,
                                TWR = txtfrecuencia_TWR.Text,
                                GRND = txtfrecuencia_GRND.Text,
                                EMERGENCY = txtfrecuencia_EMERGENCY.Text,
                                Otras = txtfrecuencia_Otras.Text,
                                Pista = txtPistas.Text,
                                Superficie_Pista = SuperficieSeleccionado.NameSuperficie,
                                Elevacion = txtElevación.Text + " " + "m" + " " + "/" + " " + txtElevaciónpies.Text + " " + "pies",
                                ASDA_Rwy_1 = Convert.ToInt16(txtASDA_RWAY_1.Text),
                                ASDA_Rwy_2 = Convert.ToInt16(txtASDA_RWAY_2.Text),
                                TODA_Rwy_1 = Convert.ToInt16(txtTODA_RWAY_1.Text),
                                TODA_Rwy_2 = Convert.ToInt16(txtTODA_RWAY_2.Text),
                                TORA_Rwy_1 = Convert.ToInt16(txtTORA_RWAY_1.Text),
                                TORA_Rwy_2 = Convert.ToInt16(txtTORA_RWAY_2.Text),
                                LDA_Rwy_1 = Convert.ToInt16(txtLDA_RWAY_1.Text),
                                LDA_Rwy_2 = Convert.ToInt16(txtLDA_RWAY_2.Text),
                                nombre_pdf = nombreCompleto,
                                Extension = "pdf",
                               Contenido = base64File

                            };
                            #region "validations"

                            if (string.IsNullOrEmpty(airportX.nombre_pdf))
                            {
                                airportX.nombre_pdf = "No disponible";
                            }

                            // Validación de Extension
                            if (string.IsNullOrEmpty(airportX.Extension))
                            {
                                airportX.Extension = "No disponible";
                            }




                            if (airportX.ATIS == "No disponible" || airportX.ATIS == "")
                            {
                                airportX.ATIS = "0.00";
                            }
                            if (airportX.TWR == "No disponible" || airportX.TWR == "")
                            {
                                airportX.TWR = "0.00";
                            }
                            if (airportX.GRND == "No disponible" || airportX.GRND == "")
                            {
                                airportX.GRND = "0.00";
                            }


                            if (airportX.EMERGENCY == "No disponible" || airportX.EMERGENCY == "")
                            {
                                airportX.EMERGENCY = "0.00";
                            }
                            if (airportX.Otras == "No disponible" || airportX.Otras == "")
                            {
                                airportX.Otras = "0.00";
                            }
                            #endregion
                            //storage the list of frequencies and the list of type_frequencies in two array
                            string[] Data_FrecPut = string.Join("\n", airportId.Frecuencias.Select(c => c.FrecuenciaFrecuencia)).Split(Convert.ToChar("\n"));
                            string[] Data_FrecPut2 = string.Join("\n", airportId.Frecuencias.Select(c => c.TipoFrecuencia)).Split(Convert.ToChar("\n"));
                            string[] Ejecutables = new string[7];
                            #region "Data validation"

                            if ((airportX.Nombre_OACI) != (airportId.Aeropuerto.NombreOaci) || (airportX.NombreICAO) != (airportId.Aeropuerto.NombreIcao) ||
                                (airportX.Estado_Aeropuerto) != (airportId.Aeropuerto.EstadoAeropuerto))
                            {
                                Ejecutables[0] = "1";
                            }
                            else
                            {
                                Ejecutables[0] = "0";
                            }

                            if ((airportX.Notam.ToString()) != string.Join("\n", airportId.NOTAMS.Select(c => c.NotamNotam)))
                            {
                                Ejecutables[1] = "2";
                            }
                            else
                            {
                                Ejecutables[1] = "0";
                            }

                            if ((airportX.Publico) != (airportId.Caracteristicas_Especiales.Publico.ToString()) || (airportX.Controlado) != (airportId.Caracteristicas_Especiales.Controlado.ToString()) ||
                                (airportX.Coordenada) != (airportId.Caracteristicas_Especiales.Coordenada) || (airportX.Info_Torre) != (airportId.Caracteristicas_Especiales.InfoTorre) ||
                                (airportX.Info_General) != (airportId.Caracteristicas_Especiales.InfoGeneral) || (airportX.Espacio_Aereo) != (airportId.Caracteristicas_Especiales.EspacioAereo) ||
                                (airportX.Combustible) != (airportId.Caracteristicas_Especiales.Combustible) || (airportX.Norma_General) != (airportId.Caracteristicas_Especiales.NormaGeneral) ||
                                (airportX.Norma_Particular) != (airportId.Caracteristicas_Especiales.NormaParticular))
                            {
                                Ejecutables[2] = "3";
                            }
                            else
                            {
                                Ejecutables[2] = "0";
                            }

                            if ((airportX.Direccion_Exacta) != (airportId.Contacto.DireccionExacta) ||
                                (airportX.Numero_Telefono1) != (airportId.Contacto.NumeroTelefono1) ||
                                (airportX.Numero_Telefono2) != (string)(airportId.Contacto.NumeroTelefono2) ||
                                (airportX.Horario.ToString()) != (airportId.Contacto.Horario))
                            {
                                Ejecutables[3] = "4";
                            }
                            else
                            {
                                Ejecutables[3] = "0";
                            }

                            if (Data_FrecPut.Length <= 6 && Data_FrecPut2.Length <= 9)
                            {
                                string[] tiposFrecuencia = { "TWR", "ATIS", "GRND", "EMERGENCY", "Otras" };

                                for (int i = 0; i < Data_FrecPut.Length; i++)
                                {
                                    int tipoIndex = Array.IndexOf(tiposFrecuencia, Data_FrecPut2[i]);

                                    switch (tipoIndex)
                                    {
                                        case 0:
                                            if (airportX.TWR != Data_FrecPut[i])
                                                Ejecutables[4] = "5";
                                            break;
                                        case 1:
                                            if (airportX.ATIS != Data_FrecPut[i])
                                                Ejecutables[4] = "5";
                                            break;
                                        case 2:
                                            if (airportX.GRND != Data_FrecPut[i])
                                                Ejecutables[4] = "5";
                                            break;
                                        case 3:
                                            if (airportX.EMERGENCY != Data_FrecPut[i])
                                                Ejecutables[4] = "5";
                                            break;
                                        case 4:
                                            if (airportX.Otras != Data_FrecPut[i])
                                                Ejecutables[4] = "5";
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }

                            else
                            {
                                Ejecutables[4] = "0";
                            }

                            if ((airportX.Pista) != (airportId.Pistas.Pista) || (airportX.Elevacion.ToString()) != (airportId.Pistas.Elevacion) ||
                                (airportX.Superficie_Pista.ToString()) != (airportId.Pistas.SuperficiePista) || (airportX.ASDA_Rwy_1.ToString()) != (airportId.Pistas.AsdaRwy1.ToString()) ||
                                (airportX.ASDA_Rwy_2.ToString()) != (airportId.Pistas.AsdaRwy2.ToString()) || (airportX.TODA_Rwy_1.ToString()) != (airportId.Pistas.TodaRwy1.ToString()) ||
                                (airportX.TODA_Rwy_2.ToString()) != (airportId.Pistas.TodaRwy2.ToString()) || (airportX.TORA_Rwy_1.ToString()) != (airportId.Pistas.ToraRwy1.ToString()) ||
                                (airportX.TORA_Rwy_2.ToString()) != (airportId.Pistas.ToraRwy2.ToString()) || (airportX.LDA_Rwy_1.ToString()) != (airportId.Pistas.LdaRwy1.ToString()) ||
                                (airportX.LDA_Rwy_2.ToString()) != (airportId.Pistas.LdaRwy2.ToString()))
                            {
                                Ejecutables[5] = "6";
                            }
                            else
                            {
                                Ejecutables[5] = "0";
                            }


                            if (airportId.Documento == null ||
                            !string.Equals(airportX.Contenido, airportId.Documento.Contenido) ||
                            !string.Equals(airportX.nombre_pdf, airportId.Documento.nombre_pdf) ||
                            !string.Equals(airportX.Extension, airportId.Documento.Extension))
                            {
                                Ejecutables[6] = "7";
                            }

                            else
                            {
                                Ejecutables[6] = "0";
                            }
                            #endregion
                            if (await AirportServices.PutAirportAsync(string.Join("", Ejecutables), airportX.ID_Aeropuerto, airportX) == 1)
                            {
                                NotificationsServices.sendNotification(Convert.ToInt32(airportId.Aeropuerto.IdAeropuerto), airportId.Aeropuerto.Nombre);
                                await DisplayAlert("Notificación", "Los datos se han modificado con éxito", "OK");
                                Application.Current.MainPage = new MainPage();
                            }
                        }

                        isProcessing = false;
                    }
                    catch (Exception ex)
                    {
                        await DialogService.ShowErrorAsync("Notificacion", ex.Message, "OK");
                        await this.Navigation.PopModalAsync();
                        isProcessing = false;
                    }
                }
                else
                {
                    gridContainer.IsVisible = true;
                    OnAppearing();
                    isProcessing = false;
                }
            });
        }
        // this code validates the functionality of the hardware backbutton 

        protected override bool OnBackButtonPressed()
        {


            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await DisplayAlert("Notificación", "¿Realmente Desea guardar los datos?", "Si", "No");
                if (result)
                {
                    Application.Current.MainPage = new MainPage();
                }
                else
                {
                    Application.Current.MainPage = new MainPage();
                }
            });
            return true;
        }

        void OnTapGestureRecognizerTappedPublic(object sender, EventArgs args)
        {
            tapCount++;
            var imageSender = (Image)sender;

            if (tapCount % 2 == 0)
            {
                pickerPublic.Focus();
            }
            else
            {
                pickerPublic.Focus();
            }
        }

        void OnTapGestureRecognizerTappedEstado(object sender, EventArgs args)
        {
            tapCount++;
            var imageSender = (Image)sender;

            if (tapCount % 2 == 0)
            {
                pickerEstado.Focus();
            }
            else
            {
                pickerEstado.Focus();
            }
        }

        void OnTapGestureRecognizerTappedSuperficie(object sender, EventArgs args)
        {
            tapCount++;
            var imageSender = (Image)sender;

            if (tapCount % 2 == 0)
            {
                pickerSuperficie.Focus();
            }
            else
            {
                pickerSuperficie.Focus();
            }
        }

        void OnTapGestureRecognizerTappedEspacioAereo(object sender, EventArgs args)
        {
            tapCount++;
            var imageSender = (Image)sender;

            if (tapCount % 2 == 0)
            {
                pickerEspacioAereo.Focus();
            }
            else
            {
                pickerEspacioAereo.Focus();
            }
        }
        void OnTapGestureRecognizerTappedControl(object sender, EventArgs args)
        {
            tapCount++;
            var imageSender = (Image)sender;

            if (tapCount % 2 == 0)
            {
                pickerControl.Focus();
            }
            else
            {
                pickerControl.Focus();
            }
        }

        private void btnlogout_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new MainPage();
        }
        private List<Airport_Update> GetPublic()
        {
            return new List<Airport_Update>
            {
                new Airport_Update{IdP=0,NameP="NO"},
                new Airport_Update{IdP=1,NameP="SI"},
            };
        }

        private List<Airport_Update> GetSuperficie()
        {
            return new List<Airport_Update>
            {
                new Airport_Update{IdSuperficie=0,NameSuperficie="No disponible"},
                new Airport_Update{IdSuperficie=1,NameSuperficie="asfalto"},
                new Airport_Update{IdSuperficie=2,NameSuperficie="concreto"},
                new Airport_Update{IdSuperficie=3,NameSuperficie="grama"},
                new Airport_Update{IdSuperficie=4,NameSuperficie="cesped"},

            };
        }

        private List<Airport_Update> GetEstado()
        {
            return new List<Airport_Update>
            {
                new Airport_Update{IdEstado=0,Nameestado ="Abierto"},
                new Airport_Update{IdEstado=1,Nameestado="Cerrado"},


            };
        }

        private List<Airport_Update> GetEspacioAereo()
        {
            List<Airport_Update> espacioAereo = new List<Airport_Update>();

            espacioAereo.Add(new Airport_Update { IdEspacioAereo = 0, NameEspacioAereo = "No disponible" });

            for (char c = 'A'; c <= 'Z'; c++)
            {
                string nombre = c.ToString();
                espacioAereo.Add(new Airport_Update { IdEspacioAereo = espacioAereo.Count, NameEspacioAereo = nombre });
            }

            return espacioAereo;
        }
        private List<Airport_Update> GetControl()
        {
            return new List<Airport_Update>
            {
                new Airport_Update{IdC=0,NameC="NO"},
                new Airport_Update{IdC=1,NameC="SI"},
            };
        }

        private void txtPistas_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (validarMascaraPista)
            {
                var editar = (Entry)sender;
                var texto = editar.Text;

                if (texto.Length > 2 && !texto.Contains("|"))
                {
                    editar.Text = texto.Substring(0, 2) + "|" + texto.Substring(3);
                }
                else if (texto.Length > 6)
                {
                    editar.Text = texto.Substring(0, 6);
                }
                else if (texto.Length >= 3)
                {

                    var ultimosDigitos = texto.Substring(3);
                    int ultimosTresDigitos;
                    if (!int.TryParse(ultimosDigitos, out ultimosTresDigitos) || ultimosTresDigitos > 359)
                    {
                        editar.Text = texto.Substring(0, 3);
                    }
                }
            }
        }


        private void TxtElevación_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.NewTextValue) && double.TryParse(e.NewTextValue, out double metros))
            {
                double pies = metros * 3.28084;
                int piesEnteros = (int)Math.Floor(pies);
                txtElevaciónpies.Text = piesEnteros.ToString();
            }
            else
            {
                txtElevaciónpies.Text = string.Empty;
            }
        }

        #region " Mascaras de frecuencias"


        private void txtfrecuencia_TWR_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtfrecuencia_TWR.Text))
            {
                string frecuenciaIngresada = txtfrecuencia_TWR.Text;
                string frecuenciaFormateada = new string(frecuenciaIngresada.Where(c => char.IsDigit(c)).ToArray());
                if (frecuenciaFormateada.Length > 3)
                {
                    frecuenciaFormateada = frecuenciaFormateada.Insert(3, ".");
                }
                if (frecuenciaFormateada.Length >= 4 && frecuenciaFormateada.Substring(3) == ".")
                {
                    frecuenciaFormateada += "00";
                }
                else if (frecuenciaFormateada.Length > 6)
                {
                    frecuenciaFormateada = frecuenciaFormateada.Remove(6);
                }
                txtfrecuencia_TWR.Text = frecuenciaFormateada;

            }

        }

        #endregion
        public string Combustibles(bool checkbox1, bool checkbox2, bool checkbox3, bool checkbox4)
        {
            string texto = string.Empty;

            if (checkbox1)
            {
                texto += "Jet Fuel A-1/";
            }
            if (checkbox2)
            {
                texto += "AVGAS 80/";
            }
            if (checkbox3)
            {
                texto += "AVGAS 100/";
            }
            if (checkbox4)
            {
                texto += "AVGAS 100 LL/";
            }
            if (string.IsNullOrEmpty(texto))
            {
                texto = "No disponible";
            }
            else
            {
                texto = texto.TrimEnd('/');
            }

            return texto;
        }

        private void ChkHorario_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHorario.IsChecked)
            {
                txtHoraInicio.IsVisible = false;
                txtHorafinal.IsVisible = false;
                LblHinicio.IsVisible = false;
                LblHfinal.IsVisible = false;
            }
            else
            {
                txtHoraInicio.IsVisible = true;
                txtHorafinal.IsVisible = true;
                LblHinicio.IsVisible = true;
                LblHfinal.IsVisible = true;
            }
;
        }
        private string base64File;
        private async void CargarPDF_Clicked(object sender, EventArgs e)
        {
            try
            {
                //txtcontenido.Text = string.Empty;
                lblPDF.Text = string.Empty;
               
             
                FileResult fileResult = await FilePicker.PickAsync(new PickOptions
                {
                    FileTypes = FilePickerFileType.Pdf,
                    PickerTitle = "Selecciona un archivo PDF"
                });

                if (fileResult != null)
                {
                    try
                    {
                        string nombreArchivo = System.IO.Path.GetFileName(fileResult.FileName);
                        string extensionArchivo = System.IO.Path.GetExtension(nombreArchivo);


                        using (Stream stream = await fileResult.OpenReadAsync())
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            await stream.CopyToAsync(memoryStream);
                            byte[] fileBytes = memoryStream.ToArray();
                            base64File = Convert.ToBase64String(fileBytes);

                           
                            lblPDF.Text = nombreArchivo;
                         

                        }

                        await DisplayAlert("Éxito", $"Nombre: {nombreArchivo}\nExtensión: {extensionArchivo}", "Aceptar");
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("Error", $"Error: {ex.Message}", "Aceptar");
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Error: {ex.Message}", "Aceptar");
            }
        }



        private async void VerPdf_Clicked(object sender, EventArgs e)
        {
            try
            {
                
                byte[] fileBytes = Convert.FromBase64String(base64File);

                string tempFilePath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "temp.pdf");
                File.WriteAllBytes(tempFilePath, fileBytes);
                await Launcher.OpenAsync(new OpenFileRequest
                {
                    File = new ReadOnlyFile(tempFilePath)
                });
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Error al abrir el PDF: {ex.Message}", "Aceptar");
            }
        }



    }
}
