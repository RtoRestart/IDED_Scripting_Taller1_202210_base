using System.Collections.Generic;
using System.Linq;
using System;

namespace TestProject1
{
    internal class TestMethods
    {
        internal enum EValueType
        {
            Two,
            Three,
            Five,
            Seven,
            Prime
        }

        internal static Stack<int> GetNextGreaterValue(Stack<int> sourceStack)
        {
            Stack<int> result = new Stack<int>();

            int[] arrayInicial = sourceStack.ToArray();
            arrayInicial = InvertArray(arrayInicial);

            for (int i = 0; i < arrayInicial.Length ; i++)
            {
                int numeroResultado = -1;
                for (int u = i+1; u < arrayInicial.Length; u++)
                {
                    if (arrayInicial[i] < arrayInicial[u])
                    {
                        numeroResultado = arrayInicial[u];
                        break;
                    }
                }
                result.Push(numeroResultado);
            }

            return result;
        }

        private static int[] InvertArray(int[] arraySource)
        {
            int[] arrayInversed = new int[arraySource.Length];

            for (int i = arraySource.Length - 1; i >= 0; i--)
            {
                arrayInversed[i] = arraySource[arraySource.Length - i-1];
            }
            return arrayInversed;
        }

        internal static Dictionary<int, EValueType> FillDictionaryFromSource(int[] sourceArr)
        {
            Dictionary<int, EValueType> result = new Dictionary<int, EValueType>();

            for (int i = 0; i < sourceArr.Length-1; i++)
            {
                if (!result.ContainsKey(sourceArr[i]))
                {
                    result.Add(sourceArr[i], GetEValueType(sourceArr[i]));
                }
            }

            return result;
        }

        private static EValueType GetEValueType(int numero)
        {
            if(numero % 2 == 0) //ver par
            {
                return EValueType.Two;
            }
            else if(numero % 3 == 0) //ver multiplo 3
            {
                return EValueType.Three;
            }            
            else if(numero % 5 == 0) //ver multiplo 5
            {
                return EValueType.Five;
            }
            else if (numero % 7 == 0) //ver multiplo 7
            {
                return EValueType.Seven;
            }
            else //ver primo
            {
                return EValueType.Prime;
            }
        }



        internal static int CountDictionaryRegistriesWithValueType(Dictionary<int, EValueType> sourceDict, EValueType type)
        {
            int result = 0;

            foreach(KeyValuePair<int,EValueType> element in sourceDict)
            {
                if(element.Value == type)
                {
                    result++;
                }
            }

            return result;
        }

        internal static Dictionary<int, EValueType> SortDictionaryRegistries(Dictionary<int, EValueType> sourceDict)
        {
            List<int> sourceList = sourceDict.Keys.ToList();

            sourceList = BubbleSort(sourceList);
            sourceList = new List<int>(InvertArray(sourceList.ToArray()));

            Dictionary<int, EValueType> result = new Dictionary<int, EValueType>();

            for (int i = 0; i < sourceList.Count; i++)
            {
                result.Add(sourceList[i], sourceDict[sourceList[i]]);
            }

            return result;
        }

        private static List<int> BubbleSort(List<int> listaSource)
        {
            List<int> listaSorteada = new List<int>(listaSource);

            for (int i = 0; i < listaSorteada.Count; i++)
            {
                for (int u = 0; u < listaSorteada.Count-1; u++)
                {
                    if (listaSorteada[u] > listaSorteada[u + 1])
                    {
                        int valorTemporal = listaSorteada[u];
                        listaSorteada[u] = listaSorteada[u + 1];
                        listaSorteada[u+1] = valorTemporal;
                    }
                }
            }
            return listaSorteada;
        }

        internal static Queue<Ticket>[] ClassifyTickets(List<Ticket> sourceList)
        {

            List<int> listaTurnos = new List<int>();

            for (int i = 0; i < sourceList.Count; i++)
            {
                listaTurnos.Add(sourceList[i].Turn);
            }

            listaTurnos = BubbleSort(listaTurnos);

            List<Ticket> listaSortedTickets = new List<Ticket>();


            for (int i = 0; i < listaTurnos.Count; i++)
            {
                Ticket ticket = sourceList.Find(ticket => ticket.Turn == listaTurnos[i]);
                listaSortedTickets.Add(ticket);
            }
           
            Queue<Ticket>[] result = new Queue<Ticket>[3]
                { 
                    new Queue<Ticket>(),                
                    new Queue<Ticket>(),                
                    new Queue<Ticket>()                
                };

            for (int i = 0; i < listaSortedTickets.Count; i++)
            {
                switch (listaSortedTickets[i].RequestType)
                {
                    case Ticket.ERequestType.Payment:
                        result[0].Enqueue(listaSortedTickets[i]);
                        break;

                    case Ticket.ERequestType.Subscription:
                        result[1].Enqueue(listaSortedTickets[i]);
                        break;

                    case Ticket.ERequestType.Cancellation:
                        result[2].Enqueue(listaSortedTickets[i]);
                        break;

                }
            }




            return result;
        }

        internal static bool AddNewTicket(Queue<Ticket> targetQueue, Ticket ticket)
        {
            bool result = false;

            Ticket sampleTicket = targetQueue.Peek();

            if (sampleTicket.RequestType == ticket.RequestType)
            {
                targetQueue.Enqueue(ticket);
                result = true;
            }

            return result;
        }        
    }
}