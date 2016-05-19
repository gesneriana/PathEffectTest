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
    /// ������ʾPathEffect����Ч���Ļ
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
        /// �Զ���ؼ�
        /// </summary>
        class MyView : View
        {
            /// <summary>
            /// ��λ
            /// </summary>
            float phase;

            PathEffect[] effects = new PathEffect[7];

            Color[] colors;

            private Paint paint;

            Path path;

            /// <summary>
            /// �̳��ڸ���Ĺ��췽��,��Ҫ�ڴ����д����ؼ�,�����ʵ�ִ˹��췽��
            /// </summary>
            /// <param name="c"></param>
            public MyView(Context c) : base(c)
            {
                paint = new Paint();
                paint.SetStyle(Paint.Style.Stroke);
                paint.StrokeWidth = 4;  // ���û��ʴ�ϸ
                // ����,����ʼ��path
                path = new Path();
                path.MoveTo(0, 0);
                Random r = new Random();
                for (int i = 1; i <= 15; i++)
                {
                    // ����15����,����������ǵ�Y����,������������һ��path
                    path.LineTo(i * 20, (float)(r.NextDouble() * 60));
                }
                // ��ʼ��7����ɫ
                colors = new Color[] { Color.Black, Color.Blue, Color.Cyan, Color.Green, Color.Magenta, Color.Red, Color.Yellow };
            }

            /// <summary>
            /// ��д�����ondraw����,���˹��췽��Ҫ�ں���� : ��д,�����Ļص�����ʹ��base�ؼ��ּ���
            /// </summary>
            /// <param name="canvas"></param>
            protected override void OnDraw(Canvas canvas)
            {
                base.OnDraw(canvas);
                // ���������ɰ�ɫ
                canvas.DrawColor(Color.White);
                // -------���濪ʼ��ʼ��7��·��Ч��--------
                effects[0] = null;
                // ʹ�� �ս� ·��Ч��
                effects[1] = new CornerPathEffect(10);
                // ʹ�� ��ɢ�� ·��Ч��
                effects[2] = new DiscretePathEffect(3.0f, 5.0f);
                // ʹ�� ���ۺ� ·��Ч��
                effects[3] = new DashPathEffect(new float[] { 20, 10, 5, 10 }, phase);
                // ʹ�� ·�����ۺ� ·��Ч��
                Path p = new Path();
                p.AddRect(0, 0, 8, 8, Path.Direction.Ccw);
                effects[4] = new PathDashPathEffect(p, 12, phase, PathDashPathEffect.Style.Rotate);
                // ʹ�� ���� ·��Ч��
                effects[5] = new ComposePathEffect(effects[2], effects[4]);
                // ʹ�� �ܺ� ·��Ч��
                effects[6] = new SumPathEffect(effects[4], effects[3]);
                // �������ƶ���8,8����ʼ����
                canvas.Translate(8, 8);
                // ����ʹ��7�ֲ�ͬ·��Ч��,7�ֲ�ͬ����ɫ������·��
                for (int i = 0; i < effects.Length; i++)
                {
                    paint.SetPathEffect(effects[i]);
                    paint.Color = colors[i];
                    canvas.DrawPath(path, paint);
                    canvas.Translate(0, 60);
                }
                Java.Lang.Thread.Sleep(50);
                // �ı�phaseֵ,�γɶ���Ч��
                phase += 1;
                Invalidate();   // ֪ͨ��ǰ�ؼ���д
            }
        }
    }
}