//Variables locales utilizadas 
int[,] mapa = new int[20, 20]; // Matriz que representa el tablero del juego
int[] barcos = { 2, 3, 4 }; // Arreglo que contiene la longitud de cada tipo de barco
string[] nombarcos = { "Navios", "Portaaviones", "Fragatas" };
int opi = 9;

//Juego 
do
{
    try
    {
        // Menú principal
        Console.WriteLine("\t\t\t\t\t\tBIENVENIDO A BATTLESHIP!\n");
        Console.WriteLine("1. Jugar");
        Console.WriteLine("2. Instrucciones");
        Console.WriteLine("0. Salir");
        opi = int.Parse(Console.ReadLine());
        Console.Clear();

        if (opi != 0)
        {
            switch (opi)
            {
                case 1:
                    // Llamada al método para comenzar el juego
                    game(mapa, barcos, nombarcos);
                    break;
                case 2:
                    // Mostrar instrucciones
                    Console.WriteLine("\t\t\t\t\t\tINSTRUCCIONES\n");
                    Console.WriteLine("Para ganar debes hundir la flota enemiga, esto se hace acertando los disparos a cada una de las posiciones que ocupa cada barco enemigo \n");
                    Console.WriteLine("Por cada disparo acertado recibirás 15 balas y 100 puntos, pero por cada desacierto recibirás una penalización de -7 balas y -25 puntos\n ");
                    Console.WriteLine("Si te quedas sin munición antes de hundir la flota enemiga perderás\n");
                    Console.WriteLine("Presione enter para regresar al menú principal");
                    Console.ReadLine();
                    Console.Clear();
                    break;
                default:
                    Console.WriteLine("Ingresó una opción inválida");
                    break;
            }
        }
    }
    catch (Exception)
    {
        // Manejo de excepciones
        Console.Clear();
        Console.WriteLine("Opción ingresada no válida");
    }
} while (opi != 0); // Continuar el juego hasta que se seleccione la opción de salir

// Mensaje de fin de juego
Console.Clear();
Console.WriteLine("Game Over\nGracias por jugar :)");

// Sección de Métodos

// Método principal del juego
static void game(int[,] mapa, int[] barcos, string[] nombarcos)
{
    ponerbarcos(mapa, barcos); // Colocar los barcos en el tablero
    int cont = 0;
    int dadas = 0;
    int unidadesene = 29;
    int municiones = 100;
    int puntos = 0;

    // Mostrar la flota enemiga
    Console.WriteLine("Flota Enemiga:");
    Console.WriteLine(barcos[0] + " " + nombarcos[0]);
    Console.WriteLine(barcos[1] + " " + nombarcos[1]);
    Console.WriteLine(barcos[2] + " " + nombarcos[2]);
    Console.WriteLine();

    // Bucle principal del juego
    do
    {
        // Mostrar estado del juego
        Console.WriteLine("Debes acertar " + unidadesene + " veces para hundir la flota enemiga");
        Console.WriteLine("Disparos: " + cont);
        Console.WriteLine("Bajas Confirmadas: " + dadas);
        Console.WriteLine("Municiones: " + municiones);
        Console.WriteLine();
        mostrarMapa(mapa); // Mostrar el mapa del juego
        Console.ResetColor();

        try
        {
            // Solicitar al jugador que elija una casilla para disparar
            Console.WriteLine("\nElija la casilla que desea atacar (separar por espacios)\n");
            string[] posicion = Console.ReadLine().Trim().Split(' ');
            Console.Clear();
            int i = int.Parse(posicion[0]) - 1;
            int j = int.Parse(posicion[1]) - 1;

            // Procesar el disparo del jugador
            if (mapa[i, j] == 1 || mapa[i, j] == 2 || mapa[i, j] == 3)
            {
                // Disparo acertado
                mapa[i, j] = 7;
                Console.Beep();
                Console.Clear();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("¡DISTE EN EL BLANCO! + 15 balas\n");
                Console.ResetColor();
                municiones += 30;
                dadas++;
                cont++;
                unidadesene--;
                puntos += 100;
            }
            else if (mapa[i, j] == 0)
            {
                // Disparo fallado
                mapa[i, j] = 5;
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("¡FALLASTE! -7 balas\n");
                Console.ResetColor();
                municiones -= 7;
                cont++;
                puntos -= 25;
            }
            else if (mapa[i, j] == 7 || mapa[i, j] == 5)
            {
                // Disparo repetido en la misma casilla
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("¡QUÉ HACES? ¡NO DISPARES DOS VECES AL MISMO LUGAR! -7 balas\n");
                Console.ResetColor();
                municiones -= 15;
                cont++;
                puntos -= 25;
            }
        }
        catch (Exception)
        {
            // Manejo de excepciones
            Console.Clear();
            Console.WriteLine("Casilla fuera del alcance efectivo\nPresione ENTER para continuar");
            Console.ReadLine();
            Console.Clear();
        }
    } while (unidadesene != 0 && municiones > 0); // Continuar el juego mientras haya unidades enemigas y municiones disponibles

    // Mostrar resultados finales del juego
    if (municiones <= 0)
    {
        // El jugador se queda sin municiones
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine("¡HAS PERDIDO!!\nNos hemos quedado sin munición\n");
        Console.ResetColor();
    }
    else if (unidadesene <= 0)
    {
        // El jugador hunde toda la flota enemiga
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine("\n¡FELICIDADES!!\nHas hundido la flota enemiga\n");
        Console.ResetColor();
    }

    mostrarMapa(mapa); // Mostrar el mapa final
    Console.ResetColor();
    Console.WriteLine();
    Console.WriteLine("Disparos acertados:" + dadas);
    Console.WriteLine("Número de veces que disparaste: " + cont);
    Console.WriteLine("Puntos obtenidos: " + puntos);
    Console.WriteLine("\nPresione ENTER para Regresar al menú principal");
    Console.ReadLine();
    Console.Clear();
}

// Método para mostrar el mapa y los índices de los renglones
static void mostrarMapa(int[,] renglones)
{
    // Imprimir encabezado de los renglones
    Console.Write("   ");
    for (int j = 1; j <= renglones.GetLength(1); j++)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($"{j,3}");
        Console.ResetColor();
    }
    Console.WriteLine();
    Console.WriteLine();

    // Recorrer el tablero y mostrar cada posición con índices de fila y columna
    for (int i = 1; i <= renglones.GetLength(0); i++)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($"{i,3}");
        Console.ResetColor();
        for (int j = 1; j <= renglones.GetLength(1); j++)
        {
            if (renglones[i - 1, j - 1] == 0)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(string.Format("{0,3}", " "));
                Console.ResetColor();
            }
            else if (renglones[i - 1, j - 1] < 4)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(string.Format("{0,3}", "-"));
                Console.ResetColor();
            }
            else if (renglones[i - 1, j - 1] == 7)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(string.Format("{0,3}", "X"));
                Console.ResetColor();
            }
            else if (renglones[i - 1, j - 1] == 5)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(string.Format("{0,3}", "O"));
                Console.ResetColor();
            }
        }
        Console.WriteLine();
    }
}

// Método para colocar los barcos en el tablero
static void ponerbarcos(int[,] rojos, int[] tambarcos)
{
    // Para generar números aleatorios
    Random ram9 = new Random();

    // Inicializar el tablero con valores vacíos
    for (int a = 0; a < rojos.GetLength(0); a++)
    {
        for (int b = 0; b < rojos.GetLength(1); b++)
        {
            rojos[a, b] = 0;
        }
    }

    // Para cada tipo de barco
    for (int i = 0; i < tambarcos.Length; i++)
    {
        int numbarcos = tambarcos[i];

        // Generar la cantidad indicada de barcos
        for (int j = 0; j < numbarcos; j++)
        {
            int primer_i, primerj, casilla;

            // Bucle para generar una posición válida para el barco
            bool casillalibre;
            do
            {
                casillalibre = true; // Suponemos que la posición es válida hasta que se demuestre lo contrario
                primer_i = ram9.Next(0, rojos.GetLength(0));
                primerj = ram9.Next(0, rojos.GetLength(1));
                casilla = ram9.Next(2);

                // Verificar si la posición se sale de los límites del tablero
                if ((casilla == 1 && primer_i + tambarcos[i] > rojos.GetLength(0)) ||
                    (casilla == 0 && primerj + tambarcos[i] > rojos.GetLength(1)))
                {
                    casillalibre = false;
                    continue; // Reintentar generando una nueva posición
                }

                // Verificar si la posición está ocupada por otro barco
                for (int p = 0; p < tambarcos[i]; p++)
                {
                    if (casilla == 0 && rojos[primer_i, primerj + p] != 0 ||
                        casilla == 1 && rojos[primer_i + p, primerj] != 0)
                    {
                        casillalibre = false;
                        break; // Reintentar generando una nueva posición
                    }
                }
            } while (!casillalibre);

            // Colocar el barco en el tablero
            for (int c = 0; c < tambarcos[i]; c++)
            {
                if (casilla == 0)
                {
                    rojos[primer_i, primerj + c] = i + 1;
                }
                else
                {
                    rojos[primer_i + c, primerj] = i + 1;
                }
            }
        }
    }
}