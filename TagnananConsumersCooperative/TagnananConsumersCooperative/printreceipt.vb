﻿Imports System
Imports System.Data
Imports MySql.Data.MySqlClient
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Public Class printreceipt

    Public datafrom As String
    Public selectedtransacnum As String

    Private conn As String = "Data Source=localhost; Database= tcc_db; User ID =root; Password=;"
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim dsConn As New MySqlConnection(conn)
        Dim dsDA As New MySqlDataAdapter
        Dim rd As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        Dim newrd As ReportDocument = New resibo
        Dim total_unit As CrystalDecisions.CrystalReports.Engine.TextObject
        Dim total_unit2 As CrystalDecisions.CrystalReports.Engine.TextObject
        total_unit = newrd.ReportDefinition.ReportObjects("Text8")
        total_unit2 = newrd.ReportDefinition.ReportObjects("Text26")
        total_unit.Text = cart2.Label7.Text
        total_unit2.Text = cart2.Label7.Text
        Try

            Dim resibo As New DataSet

            dsConn.Open()

            Dim strSQL As String = ""
            strSQL = "SELECT CONCAT(products.prodname) as articles,CONCAT(customers.lname,', ',customers.fname,' ',customers.mname) as custname,resibo.*, resibo_products.*, products.*, customers.* FROM resibo LEFT JOIN resibo_products ON resibo.transacnum = resibo_products.transacnum LEFT JOIN products ON resibo_products.prodcode = products.prodcode LEFT JOIN customers ON resibo.custcode = customers.custcode WHERE resibo.transacnum='" & selectedtransacnum & "'"
                'strSQL = "SELECT CONCAT(products.barcode,', ',products.prodname) as articles,CONCAT(customers.lname,', ',customers.fname,' ',customers.mname) as custname,resibo.*, resibo_products.*, products.*, customers.* FROM resibo LEFT JOIN resibo_products ON resibo.transacnum = resibo_products.transacnum LEFT JOIN products ON resibo_products.prodcode = products.prodcode LEFT JOIN customers ON resibo.custcode = customers.custcode WHERE resibo.transacnum='" & selectedtransacnum & "'"
                dsDA.SelectCommand = New MySqlCommand(strSQL, dsConn)
                dsDA.Fill(resibo, "resibo")

                rd = New resibo
                rd.SetDataSource(resibo)


                'rd.Load("C:\Users\Pixe\Documens\GitHub\sysdev\V5\TagnananConsumersCooperative\TagnananConsumersCooperative\resibo.rpt")
                'rd.PrintToPrinter(1, True, 0, 0)


                'ReportView.CrystalReportViewer1.ReportSource = rd
            newrd.Load("C:\Users\Pixe\Documents\GitHub\sysdev\TagnananConsumersCooperative\TagnananConsumersCooperative\resibo.rpt")
            'newrd.Load("\\A-PC\Users\Public\Documents\V7\V5\TagnananConsumersCooperative\TagnananConsumersCooperative\resibo.rpt")
            newrd.SetDataSource(resibo)
                newrd.PrintToPrinter(1, False, 0, 0)
                'ReportView.ShowDialog()
                'ReportView.Dispose()

                dsConn.Close()
                dsConn.Dispose()

            

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub printreceipt_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MessageBox.Show(cart2.Label7.Text)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
        Call cart2.cart_Load(Me, e)
        invoice.Close()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Me.Close()

    End Sub

    Private Sub printreceipt_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        cart2.Loader()
    End Sub
End Class