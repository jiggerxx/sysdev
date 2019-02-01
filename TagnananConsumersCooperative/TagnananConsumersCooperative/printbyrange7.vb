﻿Imports System
Imports System.Data
Imports MySql.Data.MySqlClient
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine


Public Class printbyrange7
    Public adapt As New MySqlDataAdapter
    Private conn As String = "Data Source=localhost; Database= tcc_db; User ID =root; Password=;"
    Private Sub Button2_Click(sender As Object, e As EventArgs)
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub printbyrange_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadtypes()

        'ComboBox1.SelectedIndex = 0
        'ComboBox2.SelectedIndex = 0
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim dsConn As New MySqlConnection(conn)
        Dim dsDA As New MySqlDataAdapter
        Dim selecteddate As String = DateTimePicker1.Value.ToString("yyyy-MM-dd")
        Dim selecteddate2 As String = DateTimePicker2.Value.ToString("yyyy-MM-dd")
        'Dim rd As CrystalDecisions.CrystalReports.Engine.ReportDocument

        Try

            Dim d As New DataSet

            dsConn.Open()

            Dim strSQL As String = ""

            Dim query As String
            Dim physicalprodcount As New DataSet
            Dim rptDoc As CrystalDecisions.CrystalReports.Engine.ReportDocument = New physicalprodcount
            Dim fromx As CrystalDecisions.CrystalReports.Engine.TextObject
            Dim tox As CrystalDecisions.CrystalReports.Engine.TextObject
            'Dim gtotal As CrystalDecisions.CrystalReports.Engine.TextObject
            Dim prep As CrystalDecisions.CrystalReports.Engine.TextObject
            'Dim typex As CrystalDecisions.CrystalReports.Engine.TextObject

            fromx = rptDoc.ReportDefinition.ReportObjects("Text18")
            tox = rptDoc.ReportDefinition.ReportObjects("Text19")
            'gtotal = rptDoc.ReportDefinition.ReportObjects("Text36")
            prep = rptDoc.ReportDefinition.ReportObjects("Text22")
            'typex = rptDoc.ReportDefinition.ReportObjects("Text13")

            fromx.Text = selecteddate
            tox.Text = selecteddate2
            'typex.Text = ComboBox1.Text
            prep.Text = MDIParent1.user.Text

            Try
                dbconn.Close()
                dbconn.Open()

                query = "SELECT ap.prodcode AS 'Product Code', p.prodname AS 'Product Name' , SUM(ap.qty) AS 'IN', SUM(rp.qty) AS 'OUT', (SUM(ap.qty) - SUM(rp.qty)) AS 'Balance', p.srp AS 'Unit Price' FROM acquired_products AS ap LEFT OUTER JOIN resibo_products AS rp ON ap.prodcode = rp.prodcode INNER JOIN products AS p ON ap.prodcode = p.prodcode WHERE  ap.daterecieved BETWEEN '" & selecteddate & "' AND '" & selecteddate2 & "' AND p.prodtype = '" & ComboBox1.Text & "' Group BY 'Product Code' ORDER BY 'Product Code'"
                adapt = New MySqlDataAdapter(query, dbconn)
                adapt.Fill(physicalprodcount, "physicalprodcount")

                rptDoc.SetDataSource(physicalprodcount)
                dbconn.Close()
            Catch ex As Exception
                MessageBox.Show(ex.Message + "physicalprodcount")
            End Try


            ReportView.CrystalReportViewer1.ReportSource = rptDoc
            ReportView.ShowDialog()
            ReportView.Dispose()


            dsConn.Close()
            dsConn.Dispose()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click

        reports.Enabled = True
        Me.Close()

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs)
        'If ComboBox1.SelectedIndex = 0 Then
        '    ComboBox2.Enabled = True
        'Else
        '    ComboBox2.Enabled = False
        'End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        'dbconn.Close()
        'dbconn.Open()
        'Try

        '    With cmd
        '        .Connection = dbconn
        '        .CommandText = " SELECT department FROM customers WHERE custcode ='" & ComboBox1.SelectedValue.ToString & "'"
        '        dr = cmd.ExecuteReader

        '        While dr.Read
        '            TextBox1.Text = dr.Item("department")

        '        End While

        '    End With

        '    dbconn.Close()
        '    dbconn.Dispose()

        'Catch ex As Exception
        '    'MessageBox.Show(ex.Message)
        'End Try
    End Sub
End Class