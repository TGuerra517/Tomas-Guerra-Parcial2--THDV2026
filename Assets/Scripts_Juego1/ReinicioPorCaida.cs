using UnityEngine;
using UnityEngine.SceneManagement;

public class ReinicioPorCaida : MonoBehaviour
{
    [SerializeField] private float alturaMinima = -10f;

    private void Update()
    {
        if (transform.position.y < alturaMinima)
        {
            SceneManager.LoadScene(
                SceneManager.GetActiveScene().buildIndex
            );
        }
    }
}