using UnityEngine;

public class MazeCell : MonoBehaviour
{
    public bool isCorrectCell;  
    public int cellIndex; 
    public MazeManager mazeManager;  

    private void OnTriggerEnter(Collider other)
    {
        // when ar camera enter the grid
        if (other.CompareTag("MainCamera"))  
        {
            mazeManager.CheckCell(cellIndex, isCorrectCell);
        }
    }
}
