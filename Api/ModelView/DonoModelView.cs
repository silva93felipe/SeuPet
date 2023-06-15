using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ModelView
{
    public class DonoModelView
    {
        public int Id {get; set; }
        public bool Ativo {get; set; }
        private string _nome;
        private string _cpf;
        private string _telefone;
        public string Nome { 
            get => _nome; 
            
            set {
                if(value.Trim().Length == 0){
                    throw new Exception("O nome não pode ser vázio");
                }

                _nome = value;
            }
        }
        public string Cpf { 
            get => _cpf; 
            
            set {
                if(value.Trim().Replace("-", "").Replace(".", "").Length != 11){
                    throw new Exception("O cpf deve ter 11 digitos.");
                }

                _cpf = value.Trim().Replace("-", "").Replace(".", "");
            }
        }
        public string Telefone { 
            get => _telefone; 
            set {
                if(String.IsNullOrEmpty(value)){
                    throw new Exception("O telefone não pode ser vázio.");
                }

                _telefone = value.Replace("(", "").Replace(")", "").Replace("-", "");
            }
        }
 
        //public EnderecoModelView Endereco {get; set;}
        //public List<Pet> Pets {get; set;}

        public DonoModelView(){
           //Endereco = new EnderecoModelView();
            //Pets = new List<Pet>();
        }
    }
}