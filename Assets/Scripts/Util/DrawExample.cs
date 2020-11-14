namespace Util
{
    using UnityEngine;

    public class DrawExample : MonoBehaviour
    {

        public Material lineMaterial;

        void OnPostRender()
        {
            lineMaterial.SetPass(0);

            Draw.color = Color.white;

            // 2D

            // draws vertical line in the middle of the screen
            Draw.Line(new Vector2(0.5f, 0), new Vector2(0.5f, 1));

            Draw.color = Color.red;

            // draws rectangle at 200px,200px, with size of 200px*200px
            Draw.Rect(200, 200, 200, 200);

            Draw.color = Color.green;

            // draws circle at the center of the screen with 100px radius
            Draw.Circle(new Vector2(0.5f, 0.5f), 100);

            Draw.color = Color.white * 0.5f;
            // draws elliplse at the center of the screen
            Draw.Ellipse(new Vector2(0.5f, 0.5f), Draw.PixelToScreen(200, 50));

            // 3D

            // Draws a cube rotating around the scene origin
            Vector3 cubeForward = new Vector3(Mathf.Sin(Time.time), 0, Mathf.Cos(Time.time));
            Draw.Cube(Vector3.zero, Vector3.one, cubeForward, Vector3.up);

            Draw.color = new Color(0, 0.2f, 1);
            Draw.color.a = 0.2f;

            // Draws a grid on the origin
            Draw.Grid(Vector3.zero, 10);
        }
    }
}