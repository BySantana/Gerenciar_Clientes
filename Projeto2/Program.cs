using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Projeto2
{
    internal class Program
    {
        [System.Serializable]
        struct Cliente
        {
            public string nome;
            public string email;
            public string cpf;
        }
        static List<Cliente> clientes = new List<Cliente>();

        enum Menu { Listagem = 1, Adicionar, Remover, Sair }



        static void Main(string[] args)
        {
            carregar();
            bool escolheuSair = false;
            while (!escolheuSair)
            {
                Console.WriteLine("1 - Listagem \n2 - Adicionar \n3 - Remover \n4 - Sair");
                Menu opcao = (Menu)int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case Menu.Listagem:
                        listagem();
                        break;
                    case Menu.Adicionar:
                        adicionar();
                        break;
                    case Menu.Remover:
                        remover();
                        break;
                    case Menu.Sair:
                        escolheuSair = true;
                        break;
                }
                Console.Clear();
            }    
            
        }



        static void adicionar()
        {
            Cliente cliente = new Cliente();
            Console.WriteLine("Cadastro de cliente: ");
            Console.WriteLine("Nome do cliente: ");
            cliente.nome = Console.ReadLine();
            Console.WriteLine("Email do cliente: ");
            cliente.email = Console.ReadLine();
            Console.WriteLine("CPF do cliente: ");
            cliente.cpf = Console.ReadLine();

            clientes.Add(cliente);
            salvar();

            Console.WriteLine("Cadastro concluído com sucesso!");
            Console.WriteLine("Digite enter para sair.");
            Console.ReadLine();
        }

        static void listagem()
        {
            Console.WriteLine("Lista de clientes: \n");
            int i = 0;
            if (clientes.Count > 0)
            {
                foreach (Cliente cliente in clientes)
                {
                Console.WriteLine($"ID: {i}");
                Console.WriteLine($"Nome: {cliente.nome}");
                Console.WriteLine($"Email: {cliente.email}");
                Console.WriteLine($"CPF: {cliente.cpf}");
                Console.WriteLine("=============================");
                i++;
                }
            }
            else
            {
                Console.WriteLine("Nenhum cliente cadastrado");
            }
            Console.WriteLine("Digite enter para sair");
            Console.ReadLine();   
        }

        static void salvar()
        {
            FileStream stream = new FileStream("clients.dat", FileMode.OpenOrCreate);
            BinaryFormatter enconder = new BinaryFormatter();

            enconder.Serialize(stream, clientes);
            stream.Close();
        }

        static void remover()
        {
            listagem();
            Console.WriteLine("Digite o ID do cliente que será removido: ");
            int id = int.Parse(Console.ReadLine());

            if(id >= 0 && id <= clientes.Count)
            {
                clientes.RemoveAt(id);
                salvar();
            }
            else
            {
                Console.WriteLine("ID inválido, tente outra vez.");
                Console.ReadLine();
            }
        }

        static void carregar()
        {
            FileStream stream = new FileStream("clients.dat", FileMode.OpenOrCreate);

            try
            {
                BinaryFormatter enconder = new BinaryFormatter();
                clientes = (List<Cliente>)enconder.Deserialize(stream);

                if (clientes == null)
                {
                    clientes = new List<Cliente>();
                }

            }catch (Exception e)
            {
                clientes = new List<Cliente>();
            }

            stream.Close();
        }
    }
}
