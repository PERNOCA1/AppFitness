using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

using AppFitness.Models;

namespace AppFitness.ViewModels
{
    class CadastroAtividadeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        string descricao, observacoes;
        int id;
        DateTime data;
        double? peso;

        public string Descricao 
        { 
            get => descricao; 
            set
            {
                descricao = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Descricao"));
            }   
        }
        public string Observacoes 
        { 
            get => observacoes; 
            set
            {
                observacoes = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Observacoes"));
            } 
        }
        public int Id 
        { 
            get => id; 
            set
            {
                id = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Id"));
            } 
        }
        public DateTime Data 
        { 
            get => data; 
            set
            {
                data = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Data"));
            }
        }
        public double? Peso 
        {
            get => peso; 
            set
            {
                peso = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Peso"));
            } 
        }


        public ICommand NovaAtividade
        {
            get => new Command(() =>
            {
                Id = 0;
                Descricao = String.Empty;
                Data = DateTime.Now;
                Peso = null;
                Observacoes = String.Empty;

            });
        }

        public ICommand SalvarAtividade
        {
            get => new Command(async () =>
            {
                try
                {
                    Atividade model = new Atividade()
                    {
                        Descricao = this.Descricao,
                        Data = this.Data,
                        Peso = this.Peso,
                        Observacoes = this.Observacoes

                    };

                    if(this.Id == 0)
                    {
                        await App.Database.Insert(model);
                    }
                    else
                    {
                        model.Id = this.Id;

                        await App.Database.Update(model);
                    }

                    await Application.Current.MainPage.DisplayAlert("Beleza!", "Atividade Salva!", "OK");

                    await Shell.Current.GoToAsync("//MinhasAtividades");

                } catch(Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Ops", ex.Message, "OK");
                }
            });
        }
        
    }
}

