using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _3
{// 1. Интерфейсы для фигур
    public interface ICircle
    {
        void Draw(Graphics g);
    }

    public interface ISquare
    {
        void Draw(Graphics g);
    }

    public interface ITriangle
    {
        void Draw(Graphics g);
    }
    // 2. Абстрактная фабрика
    public interface IShapeFactory
    {
        ICircle CreateCircle();
        ISquare CreateSquare();
        ITriangle CreateTriangle();
    }

    public partial class Form1 : Form
    {
        private IShapeFactory shapeFactory;
        private ICircle circle;
        private ISquare square;
        private ITriangle triangle;
        private ComboBox factorySelector;
        private Panel drawPanel;

        public Form1()
        {
            // Инициализация формы
            this.Text = "Абстрактная фабрика - Фигуры";
            this.Size = new Size(400, 400);

            // Выпадающий список для выбора фабрики
            factorySelector = new ComboBox();
            factorySelector.Items.Add("Red Factory");
            factorySelector.Items.Add("Blue Factory");
            factorySelector.SelectedIndexChanged += OnFactoryChanged;
            factorySelector.Dock = DockStyle.Top;
            this.Controls.Add(factorySelector);

            // Панель для отрисовки
            drawPanel = new Panel();
            drawPanel.Dock = DockStyle.Fill;
            drawPanel.Paint += OnDrawPanelPaint;
            this.Controls.Add(drawPanel);

            // По умолчанию выбрана RedFactory
            shapeFactory = new RedFactory();
            CreateShapes();
        }

        private void OnFactoryChanged(object sender, EventArgs e)
        {
            // Меняем фабрику в зависимости от выбора пользователя
            if (factorySelector.SelectedItem.ToString() == "Red Factory")
            {
                shapeFactory = new RedFactory();
            }
            else if (factorySelector.SelectedItem.ToString() == "Blue Factory")
            {
                shapeFactory = new BlueFactory();
            }

            // Создаем новые фигуры
            CreateShapes();

            // Перерисовываем панель
            drawPanel.Invalidate();
        }

        private void CreateShapes()
        {
            // Создаем фигуры с помощью фабрики
            circle = shapeFactory.CreateCircle();
            square = shapeFactory.CreateSquare();
            triangle = shapeFactory.CreateTriangle();
        }

        private void OnDrawPanelPaint(object sender, PaintEventArgs e)
        {
            // Отрисовываем фигуры на панели
            circle?.Draw(e.Graphics);
            square?.Draw(e.Graphics);
            triangle?.Draw(e.Graphics);
        }
    

    private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
    // 3. Реализация фигур для красной фабрики
    public class RedCircle : ICircle
    {
        public void Draw(Graphics g)
        {
            g.FillEllipse(Brushes.Red, 50, 50, 100, 100); // Рисуем красный круг
        }
    }

    public class RedSquare : ISquare
    {
        public void Draw(Graphics g)
        {
            g.FillRectangle(Brushes.Red, 200, 50, 100, 100); // Рисуем красный квадрат
        }
    }

    public class RedTriangle : ITriangle
    {
        public void Draw(Graphics g)
        {
            Point[] points = { new Point(150, 200), new Point(100, 300), new Point(200, 300) };
            g.FillPolygon(Brushes.Red, points); // Рисуем красный треугольник
        }
    }
    // 4. Реализация фигур для синей фабрики
    public class BlueCircle : ICircle
    {
        public void Draw(Graphics g)
        {
            g.FillEllipse(Brushes.Blue, 50, 50, 100, 100); // Рисуем синий круг
        }
    }

    public class BlueSquare : ISquare
    {
        public void Draw(Graphics g)
        {
            g.FillRectangle(Brushes.Blue, 200, 50, 100, 100); // Рисуем синий квадрат
        }
    }

    public class BlueTriangle : ITriangle
    {
        public void Draw(Graphics g)
        {
            Point[] points = { new Point(150, 200), new Point(100, 300), new Point(200, 300) };
            g.FillPolygon(Brushes.Blue, points); // Рисуем синий треугольник
        }
    }// 5. Конкретные фабрики
    public class RedFactory : IShapeFactory
    {
        public ICircle CreateCircle() => new RedCircle();
        public ISquare CreateSquare() => new RedSquare();
        public ITriangle CreateTriangle() => new RedTriangle();
    }

    public class BlueFactory : IShapeFactory
    {
        public ICircle CreateCircle() => new BlueCircle();
        public ISquare CreateSquare() => new BlueSquare();
        public ITriangle CreateTriangle() => new BlueTriangle();
    }
    

}
