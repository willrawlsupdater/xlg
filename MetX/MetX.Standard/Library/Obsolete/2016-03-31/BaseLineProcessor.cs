﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace MetX.Library
{
    
    public abstract class BaseLineProcessor
    {
        public StreamBuilder Output;
        public StreamReader InputStream;
        
        public string DestinationFilePath;
        public string InputFilePath;
        //public int LineCount;
        public bool OpenNotepad;

        public abstract bool Start();
        public abstract bool ProcessLine(string line, int number);
        public abstract bool Finish();

        public virtual bool? ReadInput(string inputType)
        {
            
            switch (inputType.ToLower().Replace(" ", string.Empty))
            {
                case "none": // This is the equivalent of reading an empty file
                    InputStream = StreamReader.Null;
                    //Lines = new List<string> {string.Empty};
                    //LineCount = 1;
                    return true;    

                case "clipboard":
                    byte[] bytes = Encoding.UTF8.GetBytes(Clipboard.GetText());
                    InputStream = new StreamReader(new MemoryStream(bytes));
                    break;

                case "databasequery":
                    throw new NotImplementedException("Database query is not yet implemented.");
                    break;

                case "webaddress":
                    bytes = Encoding.UTF8.GetBytes(IO.HTTP.GetURL(InputFilePath));
                    InputStream = new StreamReader(new MemoryStream(bytes));
                    break;

                case "file":
                    if (string.IsNullOrEmpty(InputFilePath))
                    {
                        MessageBox.Show("Please supply an input filename.", "INPUT FILE PATH REQUIRED");
                        return null;
                    }
                    if (!File.Exists(InputFilePath))
                    {
                        MessageBox.Show("The supplied input filename does not exist.", "INPUT FILE DOES NOT EXIST");
                        return null;
                    }

                    switch ((Path.GetExtension(InputFilePath) ?? string.Empty).ToLower())
                    {
                        case ".xls":
                        case ".xlsx":
                            string sideFile = null;
                            FileInfo inputFile = new FileInfo(InputFilePath);
                            InputFilePath = inputFile.FullName;
                            Type excelType = Type.GetTypeFromProgID("Excel.Application");
                            dynamic excel = Activator.CreateInstance(excelType);
                            try
                            {
                                dynamic workbook = excel.Workbooks.Open(InputFilePath);
                                sideFile = InputFilePath
                                    .Replace(".xlsx", ".xls")
                                    .Replace(".xls", "_" + DateTime.Now.ToString("G").ToLower()
                                    .Replace(":", "")
                                    .Replace("/", "")
                                    .Replace(":", ""));
                                Console.WriteLine("Saving Excel as Tab delimited at: " + sideFile + "*.txt");

                                // 20 = text (tab delimited), 6 = csv
                                int sheetNumber = 0;
                                foreach (dynamic worksheet in workbook.Sheets)
                                {
                                    string worksheetFile = sideFile + "_" + ++sheetNumber + ".txt";
                                    Console.WriteLine("Saving Worksheet " + sheetNumber + " as: " + worksheetFile);
                                    worksheet.SaveAs(worksheetFile, 20, Type.Missing, Type.Missing, false, false, 1);
                                }

                                //workbook.SaveAs(sideFile, 20, Type.Missing, Type.Missing, false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing); 
                                workbook.Close();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                            }
                            excel.Quit();
                            if (string.IsNullOrEmpty(sideFile) || !File.Exists(sideFile)) return false;
                            InputStream = new StreamReader(File.OpenRead(sideFile));
                            break;

                        default:
                            Output = new StreamBuilder(DestinationFilePath);
                            InputStream = new StreamReader(File.OpenRead(InputFilePath));
                            break;
                    }
                    break;
            }
            if (InputStream == StreamReader.Null || InputStream.BaseStream.Length < 1 || InputStream.EndOfStream)
            {
                MessageBox.Show("The supplied input is empty.", "INPUT FILE EMPTY");
                return false;
            }

            
/*
            // This way supports both windows and linux line endings
            Lines = new List<string>(AllText
                .Replace("\r", string.Empty)
                .Split(new[] { '\n' }, 
                StringSplitOptions.RemoveEmptyEntries));

            if (Lines.Count <= 0)
            {
                MessageBox.Show("The supplied input has no non-blank lines.", "INPUT FILE EMPTY");
                return false;
            }
            LineCount = Lines.Count;
*/
            return true;
        }

        #region Ask
        public static string Ask(string title, string promptText, string defaultValue)
        {
            string value = defaultValue;
            return Ask(title, promptText, ref value) == DialogResult.Cancel
                ? null
                : value;
        }

        public static string Ask(string promptText, string defaultValue = "")
        {
            string value = defaultValue;
            return Ask("ENTER VALUE", promptText, ref value) == DialogResult.Cancel
                ? null
                : value;
        }

        public static DialogResult Ask(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }
        #endregion
    }
}