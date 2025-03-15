using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;
namespace ShapesArea
{
    internal static class FormulaLogic
    {
        public static Dictionary<String, Dictionary<String, Formula>> logicDictionary = new Dictionary<String, Dictionary<String, Formula>>
        {
            {"Одномерные фигуры", new Dictionary<String, Formula>
                {
                    {"Точка", (params List<Double> parameters) => 1 },
                    {"Линия", (params List<Double> parameters) => parameters[0] }
                } 
            },
            {"Двухмерные фигуры", new Dictionary<String, Formula>
                {
                    {"Произвольный треугольник", (params List<Double> parameters) => parameters[0] * parameters[1] / 2 },
                    {"Прямоугольный треугольник", (params List<Double> parameters) => parameters[0] * parameters[1] / 2 },
                    {"Произвольный четырёхугольник", (params List<Double> parameters) => parameters[0] * parameters[1] * Sin(parameters[2]) / 2 },
                    {"Квадрат", (params List<Double> parameters) => parameters[0] * parameters[0] },
                    {"Прямоугольник", (params List<Double> parameters) => parameters[0] * parameters[1] },
                    {"Трапеция", (params List<Double> parameters) => (parameters[0] + parameters[1]) / 2 * parameters[2] },
                    {"Параллелограм", (params List<Double> parameters) => parameters[0] * parameters[1] },
                    {"Ромб", (params List<Double> parameters) => parameters[0] * parameters[1] / 2 },
                    {"Правильный многоугольник", (params List<Double> parameters) => parameters[0] * parameters[1] },
                    {"Круг", (params List<Double> parameters) => PI * Pow(parameters[0], 2) },
                    {"Сектор круга", (params List<Double> parameters) => PI * Pow(parameters[0], 2) / 360 * parameters[1] },
                    {"Сегмент круга", (params List<Double> parameters) => Pow(parameters[0], 2) / 2 * (PI * parameters[1] / 180 - Sin(parameters[1])) },
                    {"Кольцо", (params List<Double> parameters) => PI * (Pow(parameters[0], 2) - Pow(parameters[1], 2)) },
                    {"Эллипс", (params List<Double> parameters) => PI * parameters[0] * parameters[1] }
                }
            },
            {"Трёхмерные фигуры", new Dictionary<String, Formula>
                {
                    {"Шар", (params List<Double> parameters) => 4 * PI * Pow(parameters[0], 2) },
                    {"Шаровой сегмент", (params List<Double> parameters) => 2 * PI * parameters[0] * parameters[1] },
                    {"Шаровой сектор", (params List<Double> parameters) => PI * parameters[2] * ( 2 * parameters[0] + parameters[1 ]) },
                    {"Шаровой слой", (params List<Double> parameters) => 2 * PI * parameters[1] * parameters[0] },
                    {"Тор", (params List<Double> parameters) => 4 * Pow(PI, 2) * parameters[0] * parameters[1] },
                    {"Цилиндр", (params List<Double> parameters) => 2 * PI * parameters[0] * ( parameters[1] + parameters[0] ) },
                    {"Куб", (params List<Double> parameters) => 6 * Pow(parameters[0], 2) },
                    {"Параллепипед", (params List<Double> parameters) => 2 * parameters[2] * (parameters[0] + parameters[1]) },
                    {"Конус", (params List<Double> parameters) => PI * parameters[0] * (parameters[0] + parameters[1]) },
                    {"Усечённый конус", (params List<Double> parameters) => PI * (parameters[0] + parameters[1]) * parameters[4] + parameters[2] + parameters[3] },
                    {"Произвольная пирамида", (params List<Double> parameters) => parameters[0] + parameters[1] },
                    {"Усечённая произвольная пирамида", (params List<Double> parameters) => parameters[0] + parameters[1] + parameters[2] },
                    {"Правильная пирамида", (params List<Double> parameters) => parameters[0] * parameters[1] / 2 },
                    {"Усечённая правильная пирамида", (params List<Double> parameters) => (parameters[0] + parameters[1]) * parameters[2] / 2 },
                    {"Призма", (params List<Double> parameters) => 2 * parameters[0] + parameters[1] },
                    {"Эллипсойд", (params List<Double> parameters) => 4 * PI * Pow(((Pow(parameters[0] * parameters[1], 1.6) + Pow(parameters[0] * parameters[2], 1.6) + Pow(parameters[1] * parameters[2], 1.6)) / 3), 1 / 1.6) },
                    {"Тетраэдр", (params List<Double> parameters) => Pow(parameters[0], 2) * Pow(3, 1 / 2) },
                    {"Октаэдр", (params List<Double> parameters) => 2 * Pow(parameters[0], 2) * Pow(3, 1 / 2) },
                    {"Додекаэдр", (params List<Double> parameters) => 2 * Pow(parameters[0], 2) * Pow(5 * (5 + 2 * Pow(5, 0.5)), 1 / 2) },
                    {"Икосаэдр", (params List<Double> parameters) => 5 * Pow(parameters[0], 2) * Pow(3, 1 / 2) }
                }
            }
        };
    }
}
