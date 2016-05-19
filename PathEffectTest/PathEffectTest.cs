using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;

namespace PathEffectTest
{
    /// <summary>
    /// 用于显示PathEffect绘制效果的活动
    /// </summary>
    [Activity(Label = "PathEffectTest")]
    public class PathEffectTest : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(new MyView(this));
        }

        /// <summary>
        /// 自定义控件
        /// </summary>
        class MyView : View
        {
            /// <summary>
            /// 相位
            /// </summary>
            float phase;

            PathEffect[] effects = new PathEffect[7];

            Color[] colors;

            private Paint paint;

            Path path;

            /// <summary>
            /// 继承于父类的构造方法,若要在代码中创建控件,则必须实现此构造方法
            /// </summary>
            /// <param name="c"></param>
            public MyView(Context c) : base(c)
            {
                paint = new Paint();
                paint.SetStyle(Paint.Style.Stroke);
                paint.StrokeWidth = 4;  // 设置画笔粗细
                // 创建,并初始化path
                path = new Path();
                path.MoveTo(0, 0);
                Random r = new Random();
                for (int i = 1; i <= 15; i++)
                {
                    // 生成15个点,随机生成它们的Y坐标,并将它们连成一条path
                    path.LineTo(i * 20, (float)(r.NextDouble() * 60));
                }
                // 初始化7个颜色
                colors = new Color[] { Color.Black, Color.Blue, Color.Cyan, Color.Green, Color.Magenta, Color.Red, Color.Yellow };
            }

            /// <summary>
            /// 重写父类的ondraw方法,除了构造方法要在后面加 : 重写,其他的回调方法使用base关键字即可
            /// </summary>
            /// <param name="canvas"></param>
            protected override void OnDraw(Canvas canvas)
            {
                base.OnDraw(canvas);
                // 将背景填充成白色
                canvas.DrawColor(Color.White);
                // -------下面开始初始化7种路径效果--------
                effects[0] = null;
                // 使用 拐角 路径效果
                effects[1] = new CornerPathEffect(10);
                // 使用 离散的 路径效果
                effects[2] = new DiscretePathEffect(3.0f, 5.0f);
                // 使用 破折号 路径效果
                effects[3] = new DashPathEffect(new float[] { 20, 10, 5, 10 }, phase);
                // 使用 路径破折号 路径效果
                Path p = new Path();
                p.AddRect(0, 0, 8, 8, Path.Direction.Ccw);
                effects[4] = new PathDashPathEffect(p, 12, phase, PathDashPathEffect.Style.Rotate);
                // 使用 构成 路径效果
                effects[5] = new ComposePathEffect(effects[2], effects[4]);
                // 使用 总和 路径效果
                effects[6] = new SumPathEffect(effects[4], effects[3]);
                // 将画布移动到8,8处开始绘制
                canvas.Translate(8, 8);
                // 依次使用7种不同路径效果,7种不同的颜色来绘制路径
                for (int i = 0; i < effects.Length; i++)
                {
                    paint.SetPathEffect(effects[i]);
                    paint.Color = colors[i];
                    canvas.DrawPath(path, paint);
                    canvas.Translate(0, 60);
                }
                Java.Lang.Thread.Sleep(50);
                // 改变phase值,形成动画效果
                phase += 1;
                Invalidate();   // 通知当前控件重写
            }
        }
    }
}