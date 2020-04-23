using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AS_Autodoc
{
    partial class AboutTheProgram : Form
    {
        public AboutTheProgram()
        {
            InitializeComponent();
            this.Text = String.Format("О программе {0}", AssemblyTitle);
            this.labelProductName.Text = AssemblyProduct;
            this.labelVersion.Text = String.Format("Версия {0}", AssemblyVersion);
            this.labelCopyright.Text = AssemblyCopyright;
            this.labelCompanyName.Text = "ООО \"AUTODOC.RU\"";
            this.textBoxDescription.Text = "    1. Введение\r\n" +
            "   Данная программа была разработана для сети магазинов" +
            " \"AUTODOC.RU\". В автоматизируемой системе рассматриваются процессы, происходящие внутри сети магазинов автозапчастей." +
            "Основными из которых являются – поставка, распределение поставки по магазинам и продажа автозапчастей.\r\n" +
            "   2. Функционал\r\n   Для автоматизации задач магазина, в программе разработан следующий функционал:\r\n" +
            "   - регистрация пользователей;\r\n    - ввод и корректировка данных;\r\n" +
            "   - учёт автозапчастей;\r\n   - учёт поставок;\r\n    - распределение автозапчастей по отделам сети магазина;\r\n" +
            "   - учёт продаж;\r\n  - вывод необходимых отчетов.\r\n" +
            "   3. Роли\r\n Директор: просмотр отчётов по поставке и продаже автозапчастей, просмотр заявки менеджеров.\r\n" +
            "   Менеджер: распределение поставок автозапчастей по магазинам, составление заявки на покупку автозапчастей.\r\n" +
            "   Продавец: введение учёта по продаже автозапчастей, поиск необходимой автозапчасти, приём заявки на автозапчасть\r\n." +
            "   Кладовщик: добавлять новые автозапчасти,  распределять (отпускать) автозапчасти  в магазины";
        }

        #region Методы доступа к атрибутам сборки

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        private void LabelCopyright_Click(object sender, EventArgs e)
        {

        }

        private void TextBoxDescription_TextChanged(object sender, EventArgs e)
        {

        }

        private void AboutTheProgram_Load(object sender, EventArgs e)
        {

        }

        private void LabelCompanyName_Click(object sender, EventArgs e)
        {

        }

        private void OkButton_Click(object sender, EventArgs e)
        {

        }
    }
}
