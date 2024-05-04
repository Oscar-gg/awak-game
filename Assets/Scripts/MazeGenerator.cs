using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public int width, height;
    public GameObject wallPrefab; // Prefab para las paredes del laberinto
    public GameObject chestPrefab; // Prefab para el cuarto especial de recompensas (cofre)
    public int roomSize = 10; // Tamaño de cada cuarto del laberinto
    public int numChests = 5; // Número de cuartos especiales con cofres
    private int[,] maze;
    private List<Vector2> chestPositions = new List<Vector2>();

    void Start()
    {
        maze = new int[width, height];
        InitializeMaze();
        DFS(new Vector2((int)(width / 2), (int)(height / 2)));
        CreateMaze();
    }

    void InitializeMaze()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                maze[x, y] = 1; // Iniciar todas las celdas como paredes
            }
        }
    }

    void CreateMaze()
    {
        Vector3 cameraPosition = Camera.main.transform.position;

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (maze[i, j] == 1) // Paredes normales
                {
                    // Instanciar una pared como GameObject desde el prefab
                    GameObject wall = Instantiate(wallPrefab, new Vector3(i * roomSize, j * roomSize, 0), Quaternion.identity);
                    SpriteRenderer wallRenderer = wall.GetComponent<SpriteRenderer>();
                    if (wallRenderer != null)
                    {
                        wallRenderer.sprite = wallRenderer.sprite; // Asignar el sprite de la pared
                    }

                    // Alinear con la cámara para que esté visible en la escena
                    if (Vector3.Distance(wall.transform.position, cameraPosition) > 15f)
                    {
                        wallRenderer.enabled = false; // Ocultar si está muy lejos de la cámara
                    }
                }
                else if (maze[i, j] == 2) // Cuartos especiales (con cofres)
                {
                    if (chestPositions.Count < numChests)
                    {
                        // Instanciar un cuarto especial como GameObject desde el prefab
                        GameObject chestRoom = Instantiate(chestPrefab, new Vector3(i * roomSize, j * roomSize, 0), Quaternion.identity);
                        chestPositions.Add(new Vector2(i * roomSize, j * roomSize));
                    }
                }
            }
        }
    }

    void DFS(Vector2 tile)
    {
        Stack<Vector2> stack = new Stack<Vector2>();
        stack.Push(tile);

        while (stack.Count > 0)
        {
            tile = stack.Pop();
            maze[(int)tile.x, (int)tile.y] = 0; // Marcar como espacio abierto

            foreach (var direction in GetRandomDirections())
            {
                Vector2 neighbor = tile + direction;
                if (IsValid(neighbor) && maze[(int)neighbor.x, (int)neighbor.y] == 1)
                {
                    stack.Push(neighbor);
                }
            }

            // Generar cuartos especiales con cofres
            if (chestPositions.Count < numChests)
            {
                Vector2 chestRoomPosition = tile + GetRandomDirection();
                if (IsValid(chestRoomPosition))
                {
                    maze[(int)chestRoomPosition.x, (int)chestRoomPosition.y] = 2; // Marcar como cuarto especial (con cofre)
                }
            }
        }
    }

    bool IsValid(Vector2 position)
    {
        int x = (int)position.x;
        int y = (int)position.y;
        return x >= 0 && y >= 0 && x < width && y < height;
    }

    List<Vector2> GetRandomDirections()
    {
        List<Vector2> directions = new List<Vector2>
        {
            Vector2.up,
            Vector2.down,
            Vector2.right,
            Vector2.left
        };

        // Barajar las direcciones aleatoriamente
        directions.Shuffle();
        return directions;
    }

    Vector2 GetRandomDirection()
    {
        List<Vector2> directions = new List<Vector2>
        {
            Vector2.up,
            Vector2.down,
            Vector2.right,
            Vector2.left
        };

        // Barajar las direcciones aleatoriamente y devolver una dirección
        directions.Shuffle();
        return directions[0];
    }
}

// Función de extensión para barajar una lista
public static class ListExtensions
{
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
