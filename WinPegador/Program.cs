using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;


public class MiForm : Form
{
            private const int WM_RBUTTONDOWN = 0x0201; // Evento de mouse cuando se presiona la ruedita del mouse

    public MiForm()
    {
        // Configura las propiedades del form
        this.Text = "Super pegador FG";
        this.Width = 400;
        this.Height = 300;

        // Crea un Label para el título y una TextBox para la descripción
        Label tituloLabel = new Label();
        tituloLabel.Text = "FG";
        tituloLabel.Font = new Font("Arial", 16, FontStyle.Bold);
        tituloLabel.Location = new Point(50, 50);
        tituloLabel.TextAlign = ContentAlignment.MiddleCenter;

        Button boton = new Button();
        boton.Text = "Pegadeitor";
        boton.Location = new Point(50, 100);

        // Agrega el Label y el boton al form
        this.Controls.Add(tituloLabel);
        this.Controls.Add(boton);
        this.MouseClick += new MouseEventHandler(Form_MouseClick);
    }

    private void Form_MouseClick(object sender, MouseEventArgs e)
    {
        MessageBox.Show($"Clic detectado en posición: {e.Location}");
    }
    

    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new MiForm());
    }
}
