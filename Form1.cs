using System;
using System.Collections.Generic;
using static System.Math;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Text;

namespace ShapesArea
{
    public partial class Form1 : Form
    {
        private Dictionary<String, Shape> ShapeDictionary { get; } = new Dictionary<string, Shape>
        {
            //{ "Точка", new Shape((params List<Double> parameters) => { return 1; })}, 
            //{ "Линия", new Shape((params List<Double> parameters) => { return parameters[0]; }, "Длина")}, 
        };
        public Form1()
        {
            InitializeComponent();
            InitializeNodes();
        }
        private void InitializeNodes()
        {
            foreach (KeyValuePair<String, JToken> catalog in JObject.Parse(File.ReadAllText("../../shapes.json", Encoding.Default)))
            {
                TreeNode node = new TreeNode(catalog.Key);
                //MessageBox.Show(catalog.Value.ToString());

                foreach (KeyValuePair<String, JToken> shape in JObject.Parse(catalog.Value.ToString()))
                {
                    node.Nodes.Add(shape.Key);
                    ShapeDictionary[shape.Key] = new Shape
                        (
                        name: shape.Key,
                        formulaLogic: FormulaLogic.logicDictionary[catalog.Key][shape.Key],
                        formula: shape.Value["Формула"].ToObject<String>(),
                        parametersName: shape.Value["Условные обозначения"].ToObject<Dictionary<String, String>>().Select(i => $"{i.Key} ({i.Value})").ToList<String>()
                        );
                }
                treeView.Nodes.Add(node);
            }
        }
        private void TreeViewAfterSelect(object sender, TreeViewEventArgs e)
        {
            if (selectIgnore.Contains(e.Node.Text)) return;
            SetGridData(ShapeDictionary[e.Node.Text]);
            currentShapetextBox.Text = e.Node.Text;
            formulatextBox.Text = ShapeDictionary[e.Node.Text].Formula;
        }
        private List<String> selectIgnore = new List<String>() { "Одномерные фигуры", "Двухмерные фигуры", "Трёхмерные фигуры" };

        private void SetGridData(Shape shape)
        {
            dataGridView.EditMode = DataGridViewEditMode.EditOnEnter;
            dataGridView.Rows.Clear();
            foreach (String parameter in shape.ParametersName) dataGridView.Rows.Add(parameter);

        }

        private void Calculate(object sender, EventArgs e)
        {
            try
            {
                List<Double>? parameters = GetGridDataParameters();
                if (parameters is null) { MessageBox.Show("Пожалуйста, заполните все поля!"); return; }
                textBox.Text = ShapeDictionary[treeView.SelectedNode.Text].Calculate(GetGridDataParameters()).ToString() + " (см)";
            }
            catch
            {
                MessageBox.Show("Пожалуйста, выберите фигуру!"); return;
            }
           
        }

        private List<Double>? GetGridDataParameters()
        {
            List<Double> parameters = new List<Double>();
            for (Byte I = 0; I < dataGridView.Rows.Count; I++)
            {
                Object parameter = dataGridView.Rows[I].Cells[1].Value;
                if (parameter is null) return null;
                parameters.Add(Double.Parse(parameter.ToString()));
            }
            return parameters;
        }

        private void Clear(object sender, EventArgs e)
        {
            textBox.Clear();
        }
    }

    struct Shape
    {
        public String Name { get; }
        public List<String> ParametersName { get; }
        private Formula FormulaLogic { get; }
        public String Formula { get; }

        public Shape(String name, Formula formulaLogic, String formula, List<String> parametersName)
        {
            Name = name;
            ParametersName = parametersName;
            Formula = formula;
            FormulaLogic = formulaLogic;
        }

        public Double Calculate(params List<Double> parameters)
        {
            try { checked { return FormulaLogic(parameters); } }
            catch { MessageBox.Show("Объект Double был переполнен. Попробуйте ввести данные поменьше."); return 0; }
        }
    }

    delegate Double Formula(params List<Double> parameters);

}
