int[] longitud_matriz = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
string[,] matriz = new string [longitud_matriz.Length, longitud_matriz.Length];
(int x, int y)[] ingresos = [];
int cantidad = 15;
int cuenta_total = 0;
void Titulo()
{
    
    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.WriteLine("\"BUSCA X\"");
    Console.WriteLine();
    Console.ResetColor();
}
void Consigna( int cantidad, int intento_restante)
{
    if (intento_restante == 3)
    {
        Console.WriteLine("¡Bienvenido!");
        Console.WriteLine();
    }
    Console.WriteLine("Tu misión es encontrar las " + cantidad + " \"X\" escondidas en la siguiente matriz," +
        " ingresando "+cantidad+" coordenadas por intento.");
    if (!(intento_restante == 1))
    {
        Console.WriteLine("Tenes " + intento_restante + " intentos para lograrlo ¡Buena suerte!");
    }
    else
    {
        Console.WriteLine("Tenes " + intento_restante + " intento para lograrlo ¡Buena suerte!");
    }
    Console.WriteLine();
}
(int x, int y) ObtenerCoordenada(int cant)
{
    string pedido = "ingresa la fila de la " + cant + "° coordenada.";
    string reintento = "Ingreso no válido. Por favor ingrese un número del 1 al 10";
    int fila = ObtenerNumero(pedido, reintento, longitud_matriz);
    string pedido2 = "ingresa la columna de la " + cant + "° coordenada.";
    string reintento2 = "Ingreso no válido. Por favor ingrese un número del 1 al 10";
    int columna = ObtenerNumero(pedido2, reintento2, longitud_matriz);
    return (columna, fila);
}
string ContinuarJugando()
{
    string? tecla;
    Console.WriteLine("Para volver a jugar, presionar la tecla 1.");
    Console.WriteLine("Para cerrar el juego, presionar cualquier otra tecla.");
    tecla = Console.ReadKey().KeyChar.ToString();
    Console.Clear();
    return tecla;
}
string[,] CargarAsterisco(string[,] matriz)
{
    for (int fila = 1; fila <= matriz.GetLength(1); fila++)
    {
        for (int columna = 1; columna <= matriz.GetLength(0); columna++)
        {
            matriz[columna - 1, fila - 1] = "*";
        }
    }
    return matriz;
}
void MostrarMatriz(string[,] matriz, int[] longitudes, string[,] matriz_resuelta)
{
    Console.Write("   |");
    foreach (int longitud in longitudes)
    {
        Console.Write(longitud.ToString().PadLeft(4));
    }
    Console.WriteLine(" |");
    Console.WriteLine("----------------------------------------------");
    for (int fila = 1; fila <= matriz.GetLength(1); fila++)
    {
        Console.Write(fila.ToString().PadLeft(3) + "|");
        for (int columna = 1; columna <= matriz.GetLength(0); columna++)
        {
            if (matriz[columna - 1, fila - 1] == "X")
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else if (matriz[columna - 1, fila - 1] == "*")
            {
                ColorearAdyacentes((columna - 1, fila - 1), matriz_resuelta);
            }
            Console.Write(matriz[columna- 1, fila - 1].PadLeft(4));
            Console.ResetColor();
           
        }
        Console.WriteLine(" |");
        Console.ResetColor();
    }
    Console.WriteLine("");
}
string[,] CargarX(string[,] matriz, int cantidad)
{
    for(int cant =1; cant <= cantidad; cant++)
    {
        Random random = new Random();
        int dato_fila;
        int dato_columna;
        do
        {
            dato_fila = random.Next(1,10);
            dato_columna = random.Next(1,10);
        } while (matriz[dato_columna-1, dato_fila-1] == "X");
        matriz[dato_columna-1, dato_fila-1] = "X";
    }
    return matriz;
}
string[,] OcultarDatos(string[,] matriz)
{
    string[,] matriz_oculta = new string[matriz.GetLength(0), matriz.GetLength(1)]; ;
    for (int fila = 1; fila <= matriz.GetLength(1); fila++)
    {
        for (int columna = 1; columna <= matriz.GetLength(0); columna++)
        {
            matriz_oculta[columna-1,fila-1] = "#";
        }
    }
    return matriz_oculta;
}
int ObtenerNumero(string texto1, string texto2, int[] arreglo)
{
    Console.WriteLine("Por favor, " + texto1);
    bool es_valido = int.TryParse(Console.ReadLine(), out int numero);
    while (!es_valido || !Array.Exists(arreglo, elem => elem == numero))
    {
        Console.WriteLine(texto2);
        es_valido = int.TryParse(Console.ReadLine(), out numero);
    }
    return numero;
}
string[,] CompararCoincidencias((int x, int y)[] pares, string[,] matriz_con_x, string[,] matriz_oculta)
{
    foreach ((int x, int y) in pares )
    {
        matriz_oculta[x-1,y-1] = matriz_con_x[x-1,y-1];
    }
    return matriz_oculta;
}
int ContarCoincidencias(string[,] matriz_oculta)
{
    int cuenta_aciertos = 0;
    for (int fila = 1; fila <= matriz.GetLength(1); fila++)
    {
        for (int columna = 1; columna <= matriz.GetLength(0); columna++)
        {
            if (matriz_oculta[columna - 1, fila - 1] == "X")
            {
                cuenta_aciertos++;
            }
        }
    }
    return cuenta_aciertos;
}
void ColorearAdyacentes((int x, int y) ingreso, string[,] matriz)
{
    bool xCercana = false;
    for (int i = -1; i <= 1; i++)
    {
        for (int j = -1; j <= 1; j++)
        {
            int nuevoX = ingreso.x + i;
            int nuevoY = ingreso.y + j;
            if (nuevoX >= 0 && nuevoX < matriz.GetLength(0) && nuevoY >= 0 && nuevoY < matriz.GetLength(1))
            {
                if (matriz[nuevoX, nuevoY] == "X")
                {
                    xCercana = true;
                }

            }
        }
    }
    if (xCercana)
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
    }
}

string? continuar;
do
{
    cuenta_total = 0;
    ingresos = [];
    string[,] matriz_inicial = CargarAsterisco(matriz);
    string[,] matriz_con_x = CargarX(matriz, cantidad);
    string[,] matriz_oculta = OcultarDatos(matriz);
    int intento = 0;
    do
    {
        int intento_restante = 3 - intento;
        for (int cant = 1; cant <= cantidad; cant++)
        {
            Titulo();
            Consigna(cantidad, intento_restante);
            MostrarMatriz(matriz_oculta, longitud_matriz, matriz_con_x);
            Console.WriteLine("Cantidad de \"X\" encontradas: " + cuenta_total);
            (int columna, int fila) = ObtenerCoordenada(cant);
            ingresos = [.. ingresos, (columna, fila)];
            string[,] comparar_coincidencias = CompararCoincidencias(ingresos, matriz_con_x, matriz_oculta);
            cuenta_total = ContarCoincidencias(matriz_oculta);
            Console.Clear();
        }
        intento++;
    }
    while (intento < 3 && cuenta_total < cantidad);
    if (cuenta_total < cantidad)
    {
        Titulo();
        MostrarMatriz(matriz_oculta, longitud_matriz, matriz_con_x);
        Console.WriteLine("Cantidad de \"X\" encontradas: " + cuenta_total);
        Console.WriteLine();
        Console.WriteLine("Han finalizado tus intentos. Por favor presione cualquier tecla para continuar.");
        string? tecla = Console.ReadKey().KeyChar.ToString();
        Console.Clear();
    }
    Titulo();
    if (cuenta_total == cantidad)
    {
        Console.WriteLine("¡Felicidades! Encontraste todas las \"X\" ocultas y ganaste el juego. ¡Excelente trabajo!");
        Console.WriteLine();
        MostrarMatriz(matriz_con_x, longitud_matriz, matriz_con_x);
    }
    else
    {
        Console.WriteLine("Solo encontraste " + cuenta_total + " \"X\" del total de " + cantidad);
        Console.WriteLine("Agotaste tus 3 intentos y no lograste encontrar todas las \"X\" ocultas.");
        Console.WriteLine("No te desanimes. ¡Mejor suerte la próxima vez!");
        Console.WriteLine();
        MostrarMatriz(matriz_con_x, longitud_matriz, matriz_con_x);
    }
    Console.WriteLine();
    continuar = ContinuarJugando();
}
while (continuar == "1");
