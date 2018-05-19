using ControleFamiliar.Negocio;
using System;
using System.Collections.Generic;
using System.Data;

namespace ControleFamiliar.Mapeadores
{
    public class MapeadorDeItens
    {
        public bool SalvarItem(Item item)
        {
            using (var transacao = Connection.ObtenhaFbTransaction())
            {
                var comando = Connection.ObtehaComando(ObtenhaSqlDeInsercao(item));
                comando.Transaction = transacao;

                try
                {
                    comando.ExecuteNonQuery();
                    transacao.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        private static string ObtenhaSqlDeInsercao(Item item)
        {

            return $@"INSERT INTO ITEM (NOME_ITEM, QTDEMINIMA_ITEM, PRECO_ITEM,TIPOUNIDADE_ITEM)
            VALUES('{item.Descricao}','{item.QuantidadeMinimaPorItem}','{item.ObtenhaPrecoFormatado()}','{item.TipoUnidade}')";
        }

        public List<Item> ObtenhaItensCadastrados()
        {
            var listaDeItens = new List<Item>();
            using (var transacao = Connection.ObtenhaFbTransaction())
            {
                var comando = Connection.ObtehaComando("SELECT * FROM ITEM");
                comando.Transaction = transacao;
                var dataReader = comando.ExecuteReader();

                while (dataReader.Read())
                {
                    listaDeItens.Add(MapeieItem(dataReader));
                }
            }

            return listaDeItens;
        }

        private static Item MapeieItem(IDataRecord dataReader)
        {
            return new Item
            {
                Id = dataReader.GetInt32(0),
                Descricao = dataReader.GetString(1),
                QuantidadeMinimaPorItem = dataReader.GetInt32(2),
                Preco = dataReader.GetDecimal(3),
                //QuantidadeEmEstoque = dataReader.IsDBNull(4) ? 0 : dataReader.GetInt16(4),
                TipoUnidade = dataReader.GetString(4)
            };
        }

        public Item ObtenhaItemPorId(Item item)
        {
            var itemRetorno = new Item();
            using (var transacao = Connection.ObtenhaFbTransaction())
            {
                var comando = Connection.ObtehaComando($"SELECT * FROM ITEM WHERE NOME_ITEM = '{item.Descricao}' ");
                comando.Transaction = transacao;
                var dr = comando.ExecuteReader();

                while (dr.Read())
                {
                    itemRetorno = MapeieItem(dr);
                }
            }

            return itemRetorno;
        }
    }
}