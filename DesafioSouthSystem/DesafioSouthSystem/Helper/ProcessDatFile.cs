using DesafioSouthSystem.Enums;
using DesafioSouthSystem.Models;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace DesafioSouthSystem.Helper
{
    public class ProcessDatFile
    {
        public static void ReadAndWriteDatFile(string filePath)
        {
            try
            {
                string fileName;
                List<Client> clients;
                List<Salesman> salesmen;
                List<Sale> sales;
                ReadMethod(filePath, out fileName, out clients, out salesmen, out sales);
                WriteMethod(fileName, clients, salesmen, sales);
                if (File.Exists(filePath))
                {
                    //Deleta o Arquivo processado.
                    File.Delete(filePath);
                }
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
        }

       
        /// <summary>
        /// Lê o arquivo encontrado no diretório de entrada
        /// </summary>
        /// <param name="filePath">Caminho do arquivo</param>
        /// <param name="fileName">Nome do arquivo</param>
        /// <param name="clients">Lista de Clientes</param>
        /// <param name="salesmen">Lista de vendedores</param>
        /// <param name="sales">Lista de vendas</param>
        private static void ReadMethod(string filePath, out string fileName, out List<Client> clients, out List<Salesman> salesmen, out List<Sale> sales)
        {
            fileName = Path.GetFileNameWithoutExtension(filePath);
            clients = new List<Client>();
            salesmen = new List<Salesman>();
            sales = new List<Sale>();
            //var file = new StreamReader(filePath, System.Text.Encoding.Default);
            var contents = "";
            using (StreamReader file = new StreamReader(filePath))
            {
                contents = file.ReadToEnd().Trim();
            }

            //var contents = file.ReadToEnd().Trim();
            var lines = Regex.Split(contents, "\\n+", RegexOptions.None);
            foreach (string line in lines)
            {
                ProcessLine(clients, salesmen, sales, line);
            }
        }
        /// <summary>
        /// Cria o arquivo no diretório de saída.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="clients"></param>
        /// <param name="salesmen"></param>
        /// <param name="sales"></param>
        private static void WriteMethod(string fileName, List<Client> clients, List<Salesman> salesmen, List<Sale> sales)
        {
            // Trabalhar o resultado e gerar novos arquivos
            string pathNewFile = $"{Environment.GetEnvironmentVariable("HOMEPATH")}{ConfigurationManager.AppSettings["OutDirectory"]}\\{fileName}.done.dat";

            if (!File.Exists(pathNewFile))
            {
                using (StreamWriter sw = File.CreateText(pathNewFile))
                {
                    var maxSale = sales.MaxBy(i => i.TotalSale);
                    var salesmanName = sales.Find(x => x.TotalSale == sales.Min(i => i.TotalSale)).SalesmanName;

                    sw.WriteLine($"Quantidade de clientes no arquivo de entrada: {clients.Count}");
                    sw.WriteLine($"Quantidade de vendedor no arquivo de entrada: {salesmen.Count}");
                    sw.WriteLine($"ID da venda mais cara: { maxSale.SaleId }");
                    sw.WriteLine($"O pior vendedor: { salesmanName }");
                }
            }
        }

        /// <summary>
        /// Processa os dados linha por linha
        /// </summary>
        /// <param name="clients">Lista de Clientes</param>
        /// <param name="salesmen">Lista de vendedores</param>
        /// <param name="sales">Lista de vendas</param>
        /// <param name="line">Linhas do arquivo</param>
        private static void ProcessLine(List<Client> clients, List<Salesman> salesmen, List<Sale> sales, string line)
        {
            var itens = line.Replace("\r", "").Split('ç');
            var dataType = itens[0];
            switch (((DataTypeEnum)int.Parse(dataType)))
            {
                case DataTypeEnum.Salesman:
                    var salesman = new Salesman(itens[1].ToString(), itens[2].ToString(), itens[3].ToString());
                    salesmen.Add(salesman);
                    break;
                case DataTypeEnum.Client:
                    var client = new Client(itens[1].ToString(), itens[2].ToString(), itens[3].ToString());
                    clients.Add(client);
                    break;
                case DataTypeEnum.Sales:
                    var productItems = itens[2].Replace("[", "").Replace("]", "").Split(',');
                    var products = new List<Product>();
                    decimal totalSale = 0;
                    foreach (var productItem in productItems)
                    {
                        var splitProduct = productItem.Split('-');
                        int.TryParse(splitProduct[0], out var productId);
                        int.TryParse(splitProduct[0], out var productQuantity);
                        decimal.TryParse(splitProduct[2], out var price);
                        var product = new Product(productId, productQuantity, price);
                        totalSale += price;
                        products.Add(product);
                    }
                    int.TryParse(itens[1], out var saleId);

                    var sale = new Sale(saleId, itens[3].ToString(), totalSale, products);
                    sales.Add(sale);
                    break;
                default:
                    break;
            }
        }
    }
}