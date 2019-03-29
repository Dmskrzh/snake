using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake
{
    class Snake : Figure
    {
        public Direction direction;

        public Snake( Point tail, int lenght, Direction _direction)
        { // конструктор для формирования первого положения змейки
            direction = _direction;
            pList =  new List<Point>(); // координаты точек змейки хрантся в очереди
            for (int i = 0; i < lenght; i++)
            {
                Point p = new Point( tail );
                p.Move( i, direction );
                pList.Add(p);
            }
        }

        internal void Move()
        { // движение змейки удаляем хвост на единицу увличиваем голову в заданном направлении
            Point tail = pList.First();
            pList.Remove(tail);
            Point head = GetNextPoint();
            pList.Add(head);

            tail.Clear();
            head.Draw(); 
        }

        public Point GetNextPoint()
        { // находим координаты следующей точки за головой змеи в заданом нааправлении
            Point head = pList.Last();
            Point nextPoint = new Point(head);
            nextPoint.Move(1, direction);
            return nextPoint;
        }

        public void HandleKey(ConsoleKey key)
        { // опрелелим направление заданное стрелками, сохраним в переменную direction
            if (key == ConsoleKey.LeftArrow)
                direction = Direction.LEFT;
            else if (key == ConsoleKey.RightArrow)
                direction = Direction.RIGHT;
            else if (key == ConsoleKey.DownArrow)
                direction = Direction.DOWN;
            else if (key == ConsoleKey.UpArrow)
                direction = Direction.UP;
        }

        public bool Eat(Point food)
        {
            Point head = GetNextPoint();
            if (head.IsHit(food))
            {
                food.sym = head.sym; // меняем символ еды на символ головы змеи
                pList.Add(food);     // записываем новое значение в очередь - удлиняем тело змеи
                return true;
            }
            else
                return false;
        }
    }
}