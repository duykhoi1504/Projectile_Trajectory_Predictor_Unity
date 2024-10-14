• Vẽ trước line của viên đạn
    -> kéo prefab DrawLine trong thư mục Edtior vào scene
    ->  Start Point :mặc định sẽ lấy vị trí hiện tại của prefab trên scene làm điểm bắt đầu nếu không quy định trước
    ->  Target Point :mặc định sẽ lấy theo tọa độ chuột nếu không quy định trước
    -> điều chỉnh Duration,HeightY trại Prefab DrawLine sẽ không ảnh thưởng đến đường bay thật của viên đạn
    -> Curent Bullet Type: lựa chọn loại đạn sẽ vẽ trên scene
    -> để reset lại bản đầu -> chuột phải vào Script "DrawLineGizmos" ->  ReSetup
    -> xem ví dụ ở Sample/Scenes/TestBullet(scene)
• Bullet 
    - chỉnh duraion và Height trong prefab (Runtime/Bullet/Prefab)
    - NormalBullet bay theo quỹ đạo ném xiên
    - KaisaBullet Bay theo hình xoắn
    - Ex:   KaisaBullet _kaisaBullet;      
            KaisaBullet kaisaBullet=Instantiate(_kaisaBullet,transform.position,Quaternion.identity);
            kaisaBullet.Init(transform.position,target.position,(a)=>Destroy(a.gameObject));
    - xem ví dụ ở Sample/Scripts/TestTrajectory.cs
• Animation Impact Material
    ->Điều kiện dùng:
        * phải có sẵn Package AllIn1SpriteShader và Dotween
        * Bật Hit Effect, chỉnh Hit Effect Blend về 0
        *Bật Offset 
    ->cách dùng: thêm component AddAllIn1Shader vào GameObject chứa sprite ảnh -> thêm component AnimMaterial
    -> gọi ra:
        EX:
          [SerializeField] private AnimMaterial enemyAnim;
            private void GetImpact()
                {
                    enemyAnim.ImpactAnim();
                }
    -> xem ví dụ ở Sample/Scenes/TestMaterialAnimation(scene)
