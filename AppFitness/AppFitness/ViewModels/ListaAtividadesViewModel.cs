using AppFitness.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppFitness.ViewModels
{
    class ListaAtividadesViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /*
         * Pegando o que foi digitado pelo usuário.
         */
        public string ParametroBuscar { get; set; }


        /*
         * Gerencia se mostra ao o usuário o RefreshView.
         */
        bool estaAtualizando = false;
        public bool EstaAtualizando
        {
            get => estaAtualizando;
            set
            {
                estaAtualizando = value;
                PropertyChanged(this, new PropertyChangedEventArgs("EstaAtualizando"));
            }
        }

        /*
         * Coleção que armazena as Atividades do usuário.
         */

        ObservableCollection<Atividade> listaAtividades = new ObservableCollection<Atividade>();
        public ObservableCollection<Atividade> ListaAtividades
        {
            get => listaAtividades;
            set => listaAtividades = value;
        }

        public ICommand AtualizarLista
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        if (EstaAtualizando)
                            return;

                        EstaAtualizando = true;

                        List<Atividade> tmp = await App.Database.GetAllRows();

                        ListaAtividades.Clear();

                        tmp.ForEach(i => ListaAtividades.Add(i));

                    }
                    catch (Exception ex)
                    {
                        await Application.Current.MainPage.DisplayAlert("Ops", ex.Message, "OK");
                    }
                    finally
                    {
                        EstaAtualizando = false;
                    }
                });
            }

        }


        public ICommand Buscar
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        if (EstaAtualizando)
                            return;

                        EstaAtualizando = true;

                        List<Atividade> tmp = await App.Database.Search(ParametroBuscar);

                        ListaAtividades.Clear();

                        tmp.ForEach(i => ListaAtividades.Add(i));

                    }
                    catch (Exception ex)
                    {
                        await Application.Current.MainPage.DisplayAlert("Ops", ex.Message, "OK");
                    }
                    finally
                    {
                        EstaAtualizando = false;
                    }
                });
            }

        }
    }
}
