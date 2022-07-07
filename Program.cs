internal class Program
{
    private static void Main(string[] args)
    {
        Dictionary<string, List<nodo>> Grafo = new Dictionary<string, List<nodo>>();
        int n_nodos = 0;
        int total = int.MaxValue;
        List<string> recorrido = new List<string>();
        List<string> caminoMomento = new List<string>() { };

        Empezar();

        void Empezar()
        {
            try
            {
                do
                {
                    Console.Clear();
                    Console.Write("Cantidad de nodos: ");
                    n_nodos = Convert.ToInt32(Console.ReadLine());
                    if (n_nodos < 0) throw new Exception("No se aceptan numeros negativos!");

                    CrearNodos(Grafo, n_nodos);
                    Console.Write("Desea ingresar mas nodos (Cualquier Tecla: si/ n: no): ");
                } while (Console.ReadLine() != "n");

                Mostrar(Grafo);
                Console.WriteLine("\nUnirNodos: ");
                UnirNodos(Grafo);
                Console.WriteLine("Mostrar Nodos unidos: ");
                Mostrar(Grafo);

                Console.WriteLine("\nMostrar Recorrido minimo: ");
                Console.Write("Inicio: ");
                string? inicio = Console.ReadLine();
                recorrido.Add(inicio);
                Console.Write("Fin: ");
                string? fin = Console.ReadLine();

                Recorrido(Grafo, caminoMomento, inicio, fin);
                Console.WriteLine($"Recorrido: {string.Join('-', recorrido)}\nTotal: {total}");

            }
            catch (Exception error)
            {
                Console.WriteLine($"Error: {error.Message}");
                Console.ReadLine();
                Empezar();
            }
        }

        void CrearNodos(Dictionary<string, List<nodo>> Lista, int n)
        {
            int n_datos = Lista.Count();
            for (int i = n_datos; i < n_datos + n; i++)
                Lista.Add(i.ToString(), new List<nodo>());
        }

        void Mostrar(Dictionary<string, List<nodo>> Lista)
        {
            foreach (var item in Lista)
            {
                Console.Write($"\n    ->{item.Key}:");
                foreach (var item2 in item.Value)
                    Console.Write($"{item2.nodoNuevo}/  ");
            }
        }

        void UnirNodos(Dictionary<string, List<nodo>> Lista)
        {
            try
            {
                do
                {
                    Console.Write("Formato(nodo:unir1,valor1;unir2,valor2;unir3,valor3;...): ");
                    string? newnodo = Console.ReadLine();
                    string[]? valor = newnodo.Split(':');
                    foreach (string item in valor[1].Split(';'))
                    {
                        if (Lista.ContainsKey(item.Split(',')[0]))
                            Lista[valor[0]].Add(new nodo { nodoNuevo = item.Split(',')[0], distancia = Convert.ToInt32(item.Split(',')[1]) });
                        else
                            throw new Exception("Datos invalidos!");
                    }
                    Console.Write("Desea ingresar mas nodos (Cualquier Tecla: si/ n: no): ");
                } while (Console.ReadLine() != "n");
            }
            catch (Exception error)
            {
                Console.WriteLine($"Error: {error.Message}");
                Console.ReadLine();
                UnirNodos(Lista);
            }
        }

        void Recorrido(Dictionary<string, List<nodo>> Lista, List<string> caminoMomento, string inicio, string fin, int momento = 0)
        {
            if (inicio == fin)
            {
                if (momento < total)
                {
                    string i = recorrido[0];
                    recorrido.Clear();
                    recorrido.Add(i);
                    recorrido.AddRange(caminoMomento);
                    total = momento;
                }
                return;
            }
            foreach (var item in Lista[inicio])
            {

                if ( caminoMomento.Contains(item.nodoNuevo))
                    continue;

                caminoMomento.Add(item.nodoNuevo);
                momento += item.distancia;

                Recorrido(Grafo, caminoMomento, item.nodoNuevo, fin, momento);
                caminoMomento.RemoveAt(caminoMomento.Count() - 1);
                momento -= item.distancia;
            }
        }
    }
}

public struct nodo{
    public string nodoNuevo {get; set; }
    public int distancia {get; set; }
}