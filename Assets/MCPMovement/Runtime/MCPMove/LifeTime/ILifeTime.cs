public interface  ILifeTime
{
    public float Time { get; set; } 

    // nếu là trail thì dùng trail.time
    // nếu là particle thì dùng particle life time
    // nếu là mesh, hoặc game object thì dùng config
    // nếu vẫn không phải, xài default
}
