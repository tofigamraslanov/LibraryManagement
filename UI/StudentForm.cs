﻿using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UI
{
    public partial class StudentForm : Form
    {
        StudentManager _studentManager = new StudentManager(new EfStudentDal());
        public StudentForm()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnBackToHomeFromLibrarians_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm mainForm = new MainForm();
            mainForm.Show();
        }

        private void StudentForm_Load(object sender, EventArgs e)
        {
            LoadStudents();
        }

        private void LoadStudents()
        {
            dgwStudents.DataSource = _studentManager.GetAll();
        }

        private void btnAddLibrarian_Click(object sender, EventArgs e)
        {
            if (tbxName.Text == "" || tbxDepartment.Text == "" || tbxCourse.Text == "" || tbxPhone.Text == "")
            {
                MessageBox.Show("Please fill out text box!");
            }
            else
            {
                _studentManager.Add(new Student
                {
                    Name = tbxName.Text,
                    Department = tbxDepartment.Text,
                    Course = int.Parse(tbxCourse.Text),
                    Phone = tbxPhone.Text
                });
                MessageBox.Show("Student added succesfuly");
            }
        }

        private void dgwStudents_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tbxName.Text = dgwStudents.CurrentRow.Cells[1].Value.ToString();
            tbxDepartment.Text = dgwStudents.CurrentRow.Cells[2].Value.ToString();
            tbxCourse.Text = dgwStudents.CurrentRow.Cells[3].Value.ToString();
            tbxPhone.Text = dgwStudents.CurrentRow.Cells[4].Value.ToString();
        }

        private void btnUpdateLibrarian_Click(object sender, EventArgs e)
        {
            _studentManager.Update(new Student
            {
                Id = Convert.ToInt32(dgwStudents.CurrentRow.Cells[0].Value),
                Name = tbxName.Text,
                Department = tbxDepartment.Text,
                Course = Convert.ToInt32(tbxCourse.Text),
                Phone = tbxPhone.Text
            });
            LoadStudents();
            MessageBox.Show("Updated Successfully");
        }

        private void btnDeleteLibrarian_Click(object sender, EventArgs e)
        {
            _studentManager.Delete(new Student
            {
                Id = Convert.ToInt32(dgwStudents.CurrentRow.Cells[0].Value)
            });
            LoadStudents();
            MessageBox.Show("Deleted Successfully");
        }
    }
}
