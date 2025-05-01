namespace Marco.Forms.UI.Client.Components;

public interface IPositioner
{
    public (double X, double Y) GetPosition();
    public void Update((double x, double y) newPosition);
}