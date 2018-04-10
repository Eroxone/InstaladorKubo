<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class InstaladorKubo
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(InstaladorKubo))
        Me.lbUnidadF = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.fbdDescarga = New System.Windows.Forms.FolderBrowserDialog()
        Me.lb64bits = New System.Windows.Forms.Label()
        Me.lbUsuario = New System.Windows.Forms.Label()
        Me.lbSistemaO = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.btSalir = New System.Windows.Forms.Button()
        Me.btTodo = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cbOffice2016odt = New System.Windows.Forms.CheckBox()
        Me.lbSoftwaredescargable = New System.Windows.Forms.Label()
        Me.cbTerceros = New System.Windows.Forms.CheckBox()
        Me.cbOffice2003 = New System.Windows.Forms.CheckBox()
        Me.cbOffice2016 = New System.Windows.Forms.CheckBox()
        Me.cbConfiguraNotin = New System.Windows.Forms.CheckBox()
        Me.lblAncert = New System.Windows.Forms.LinkLabel()
        Me.cbSferen = New System.Windows.Forms.CheckBox()
        Me.cbRequisitos = New System.Windows.Forms.CheckBox()
        Me.cbPasarelaSigno = New System.Windows.Forms.CheckBox()
        Me.cbConfiguraWord2016 = New System.Windows.Forms.CheckBox()
        Me.lbRequisitos = New System.Windows.Forms.Label()
        Me.lbPaquetes = New System.Windows.Forms.Label()
        Me.cbNemo = New System.Windows.Forms.CheckBox()
        Me.cbPuestoNotin = New System.Windows.Forms.CheckBox()
        Me.btNotinKubo = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.lbRuta = New System.Windows.Forms.Label()
        Me.btDirDescargas = New System.Windows.Forms.Button()
        Me.pbDescargas = New System.Windows.Forms.ProgressBar()
        Me.btDescargar = New System.Windows.Forms.Button()
        Me.tlpUnidadF = New System.Windows.Forms.ToolTip(Me.components)
        Me.tlpDescargas = New System.Windows.Forms.ToolTip(Me.components)
        Me.tlpOffice2016odt = New System.Windows.Forms.ToolTip(Me.components)
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'lbUnidadF
        '
        Me.lbUnidadF.AutoSize = True
        Me.lbUnidadF.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbUnidadF.ForeColor = System.Drawing.Color.DarkGray
        Me.lbUnidadF.Location = New System.Drawing.Point(4, 22)
        Me.lbUnidadF.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lbUnidadF.Name = "lbUnidadF"
        Me.lbUnidadF.Size = New System.Drawing.Size(95, 24)
        Me.lbUnidadF.TabIndex = 0
        Me.lbUnidadF.Text = "Unidad F"
        '
        'GroupBox4
        '
        Me.GroupBox4.BackColor = System.Drawing.Color.FloralWhite
        Me.GroupBox4.Controls.Add(Me.lbUnidadF)
        Me.GroupBox4.Font = New System.Drawing.Font("Bookman Old Style", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox4.Location = New System.Drawing.Point(375, 18)
        Me.GroupBox4.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupBox4.Size = New System.Drawing.Size(205, 57)
        Me.GroupBox4.TabIndex = 35
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Unidad F"
        '
        'lb64bits
        '
        Me.lb64bits.AutoSize = True
        Me.lb64bits.Font = New System.Drawing.Font("Bookman Old Style", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lb64bits.Location = New System.Drawing.Point(9, 42)
        Me.lb64bits.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lb64bits.Name = "lb64bits"
        Me.lb64bits.Size = New System.Drawing.Size(103, 16)
        Me.lb64bits.TabIndex = 26
        Me.lb64bits.Text = "Sistema x32/64"
        '
        'lbUsuario
        '
        Me.lbUsuario.AutoSize = True
        Me.lbUsuario.Font = New System.Drawing.Font("Bookman Old Style", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbUsuario.Location = New System.Drawing.Point(9, 63)
        Me.lbUsuario.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lbUsuario.Name = "lbUsuario"
        Me.lbUsuario.Size = New System.Drawing.Size(50, 15)
        Me.lbUsuario.TabIndex = 25
        Me.lbUsuario.Text = "Usuario"
        '
        'lbSistemaO
        '
        Me.lbSistemaO.AutoSize = True
        Me.lbSistemaO.Font = New System.Drawing.Font("Bookman Old Style", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbSistemaO.Location = New System.Drawing.Point(9, 22)
        Me.lbSistemaO.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lbSistemaO.Name = "lbSistemaO"
        Me.lbSistemaO.Size = New System.Drawing.Size(119, 16)
        Me.lbSistemaO.TabIndex = 0
        Me.lbSistemaO.Text = "Sistema Operativo"
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.FloralWhite
        Me.GroupBox3.Controls.Add(Me.lb64bits)
        Me.GroupBox3.Controls.Add(Me.lbUsuario)
        Me.GroupBox3.Controls.Add(Me.lbSistemaO)
        Me.GroupBox3.Font = New System.Drawing.Font("Bookman Old Style", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(30, 18)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupBox3.Size = New System.Drawing.Size(215, 88)
        Me.GroupBox3.TabIndex = 34
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Sistema"
        '
        'btSalir
        '
        Me.btSalir.Font = New System.Drawing.Font("MS Reference Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btSalir.Location = New System.Drawing.Point(594, 466)
        Me.btSalir.Margin = New System.Windows.Forms.Padding(2)
        Me.btSalir.Name = "btSalir"
        Me.btSalir.Size = New System.Drawing.Size(68, 32)
        Me.btSalir.TabIndex = 33
        Me.btSalir.Text = "Salir"
        Me.btSalir.UseVisualStyleBackColor = True
        '
        'btTodo
        '
        Me.btTodo.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btTodo.Location = New System.Drawing.Point(202, 298)
        Me.btTodo.Margin = New System.Windows.Forms.Padding(2)
        Me.btTodo.Name = "btTodo"
        Me.btTodo.Size = New System.Drawing.Size(92, 24)
        Me.btTodo.TabIndex = 19
        Me.btTodo.Text = "Marcar todos"
        Me.btTodo.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox1.Controls.Add(Me.cbOffice2016odt)
        Me.GroupBox1.Controls.Add(Me.btTodo)
        Me.GroupBox1.Controls.Add(Me.lbSoftwaredescargable)
        Me.GroupBox1.Controls.Add(Me.cbTerceros)
        Me.GroupBox1.Controls.Add(Me.cbOffice2003)
        Me.GroupBox1.Controls.Add(Me.cbOffice2016)
        Me.GroupBox1.Controls.Add(Me.cbConfiguraNotin)
        Me.GroupBox1.Controls.Add(Me.lblAncert)
        Me.GroupBox1.Controls.Add(Me.cbSferen)
        Me.GroupBox1.Controls.Add(Me.cbRequisitos)
        Me.GroupBox1.Controls.Add(Me.cbPasarelaSigno)
        Me.GroupBox1.Controls.Add(Me.cbConfiguraWord2016)
        Me.GroupBox1.Controls.Add(Me.lbRequisitos)
        Me.GroupBox1.Controls.Add(Me.lbPaquetes)
        Me.GroupBox1.Controls.Add(Me.cbNemo)
        Me.GroupBox1.Controls.Add(Me.cbPuestoNotin)
        Me.GroupBox1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.GroupBox1.Font = New System.Drawing.Font("Bookman Old Style", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(16, 110)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupBox1.Size = New System.Drawing.Size(296, 329)
        Me.GroupBox1.TabIndex = 29
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Descargas"
        '
        'cbOffice2016odt
        '
        Me.cbOffice2016odt.AutoSize = True
        Me.cbOffice2016odt.Font = New System.Drawing.Font("Bookman Old Style", 10.2!)
        Me.cbOffice2016odt.Location = New System.Drawing.Point(16, 87)
        Me.cbOffice2016odt.Name = "cbOffice2016odt"
        Me.cbOffice2016odt.Size = New System.Drawing.Size(196, 23)
        Me.cbOffice2016odt.TabIndex = 20
        Me.cbOffice2016odt.Text = "Office 2016 ODT (2,4Gb)"
        Me.cbOffice2016odt.UseVisualStyleBackColor = True
        '
        'lbSoftwaredescargable
        '
        Me.lbSoftwaredescargable.AutoSize = True
        Me.lbSoftwaredescargable.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbSoftwaredescargable.Location = New System.Drawing.Point(13, 23)
        Me.lbSoftwaredescargable.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lbSoftwaredescargable.Name = "lbSoftwaredescargable"
        Me.lbSoftwaredescargable.Size = New System.Drawing.Size(125, 17)
        Me.lbSoftwaredescargable.TabIndex = 2
        Me.lbSoftwaredescargable.Text = "Aplicación Notin"
        '
        'cbTerceros
        '
        Me.cbTerceros.AutoSize = True
        Me.cbTerceros.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold)
        Me.cbTerceros.Location = New System.Drawing.Point(14, 269)
        Me.cbTerceros.Margin = New System.Windows.Forms.Padding(2)
        Me.cbTerceros.Name = "cbTerceros"
        Me.cbTerceros.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.cbTerceros.Size = New System.Drawing.Size(211, 21)
        Me.cbTerceros.TabIndex = 18
        Me.cbTerceros.Text = "Aplicaciones de Terceros"
        Me.cbTerceros.UseVisualStyleBackColor = True
        '
        'cbOffice2003
        '
        Me.cbOffice2003.AutoSize = True
        Me.cbOffice2003.Font = New System.Drawing.Font("Bookman Old Style", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbOffice2003.Location = New System.Drawing.Point(16, 41)
        Me.cbOffice2003.Margin = New System.Windows.Forms.Padding(2)
        Me.cbOffice2003.Name = "cbOffice2003"
        Me.cbOffice2003.Size = New System.Drawing.Size(108, 23)
        Me.cbOffice2003.TabIndex = 0
        Me.cbOffice2003.Text = "Office 2003"
        Me.cbOffice2003.UseVisualStyleBackColor = True
        '
        'cbOffice2016
        '
        Me.cbOffice2016.AutoSize = True
        Me.cbOffice2016.Font = New System.Drawing.Font("Bookman Old Style", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbOffice2016.Location = New System.Drawing.Point(16, 63)
        Me.cbOffice2016.Margin = New System.Windows.Forms.Padding(2)
        Me.cbOffice2016.Name = "cbOffice2016"
        Me.cbOffice2016.Size = New System.Drawing.Size(108, 23)
        Me.cbOffice2016.TabIndex = 1
        Me.cbOffice2016.Text = "Office 2016"
        Me.cbOffice2016.UseVisualStyleBackColor = True
        '
        'cbConfiguraNotin
        '
        Me.cbConfiguraNotin.AutoSize = True
        Me.cbConfiguraNotin.Font = New System.Drawing.Font("Bookman Old Style", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbConfiguraNotin.Location = New System.Drawing.Point(142, 41)
        Me.cbConfiguraNotin.Margin = New System.Windows.Forms.Padding(2)
        Me.cbConfiguraNotin.Name = "cbConfiguraNotin"
        Me.cbConfiguraNotin.Size = New System.Drawing.Size(128, 19)
        Me.cbConfiguraNotin.TabIndex = 12
        Me.cbConfiguraNotin.Text = "Config. Notin .Net"
        Me.cbConfiguraNotin.UseVisualStyleBackColor = True
        '
        'lblAncert
        '
        Me.lblAncert.AutoSize = True
        Me.lblAncert.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblAncert.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAncert.LinkColor = System.Drawing.Color.Black
        Me.lblAncert.Location = New System.Drawing.Point(11, 203)
        Me.lblAncert.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblAncert.Name = "lblAncert"
        Me.lblAncert.Size = New System.Drawing.Size(123, 17)
        Me.lblAncert.TabIndex = 8
        Me.lblAncert.TabStop = True
        Me.lblAncert.Text = "Software Ancert"
        '
        'cbSferen
        '
        Me.cbSferen.AutoSize = True
        Me.cbSferen.Font = New System.Drawing.Font("Bookman Old Style", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbSferen.Location = New System.Drawing.Point(14, 222)
        Me.cbSferen.Margin = New System.Windows.Forms.Padding(2)
        Me.cbSferen.Name = "cbSferen"
        Me.cbSferen.Size = New System.Drawing.Size(73, 23)
        Me.cbSferen.TabIndex = 7
        Me.cbSferen.Text = "Sferen"
        Me.cbSferen.UseVisualStyleBackColor = True
        '
        'cbRequisitos
        '
        Me.cbRequisitos.AutoSize = True
        Me.cbRequisitos.Font = New System.Drawing.Font("Bookman Old Style", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbRequisitos.Location = New System.Drawing.Point(162, 154)
        Me.cbRequisitos.Margin = New System.Windows.Forms.Padding(2)
        Me.cbRequisitos.Name = "cbRequisitos"
        Me.cbRequisitos.Size = New System.Drawing.Size(130, 23)
        Me.cbRequisitos.TabIndex = 15
        Me.cbRequisitos.Text = "Pre-Requisitos"
        Me.cbRequisitos.UseVisualStyleBackColor = True
        '
        'cbPasarelaSigno
        '
        Me.cbPasarelaSigno.AutoSize = True
        Me.cbPasarelaSigno.Font = New System.Drawing.Font("Bookman Old Style", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbPasarelaSigno.Location = New System.Drawing.Point(14, 244)
        Me.cbPasarelaSigno.Margin = New System.Windows.Forms.Padding(2)
        Me.cbPasarelaSigno.Name = "cbPasarelaSigno"
        Me.cbPasarelaSigno.Size = New System.Drawing.Size(131, 23)
        Me.cbPasarelaSigno.TabIndex = 6
        Me.cbPasarelaSigno.Text = "Pasarela Signo"
        Me.cbPasarelaSigno.UseVisualStyleBackColor = True
        '
        'cbConfiguraWord2016
        '
        Me.cbConfiguraWord2016.AutoSize = True
        Me.cbConfiguraWord2016.Font = New System.Drawing.Font("Bookman Old Style", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbConfiguraWord2016.Location = New System.Drawing.Point(142, 63)
        Me.cbConfiguraWord2016.Margin = New System.Windows.Forms.Padding(2)
        Me.cbConfiguraWord2016.Name = "cbConfiguraWord2016"
        Me.cbConfiguraWord2016.Size = New System.Drawing.Size(131, 19)
        Me.cbConfiguraWord2016.TabIndex = 13
        Me.cbConfiguraWord2016.Text = "Config. Word 2016"
        Me.cbConfiguraWord2016.UseVisualStyleBackColor = True
        '
        'lbRequisitos
        '
        Me.lbRequisitos.AutoSize = True
        Me.lbRequisitos.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbRequisitos.Location = New System.Drawing.Point(159, 135)
        Me.lbRequisitos.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lbRequisitos.Name = "lbRequisitos"
        Me.lbRequisitos.Size = New System.Drawing.Size(106, 17)
        Me.lbRequisitos.TabIndex = 14
        Me.lbRequisitos.Text = "Software .Net"
        '
        'lbPaquetes
        '
        Me.lbPaquetes.AutoSize = True
        Me.lbPaquetes.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbPaquetes.Location = New System.Drawing.Point(11, 135)
        Me.lbPaquetes.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lbPaquetes.Name = "lbPaquetes"
        Me.lbPaquetes.Size = New System.Drawing.Size(76, 17)
        Me.lbPaquetes.TabIndex = 5
        Me.lbPaquetes.Text = "Paquetes"
        '
        'cbNemo
        '
        Me.cbNemo.AutoSize = True
        Me.cbNemo.Font = New System.Drawing.Font("Bookman Old Style", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbNemo.Location = New System.Drawing.Point(14, 154)
        Me.cbNemo.Margin = New System.Windows.Forms.Padding(2)
        Me.cbNemo.Name = "cbNemo"
        Me.cbNemo.Size = New System.Drawing.Size(67, 23)
        Me.cbNemo.TabIndex = 3
        Me.cbNemo.Text = "Nemo"
        Me.cbNemo.UseVisualStyleBackColor = True
        '
        'cbPuestoNotin
        '
        Me.cbPuestoNotin.AutoSize = True
        Me.cbPuestoNotin.Font = New System.Drawing.Font("Bookman Old Style", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbPuestoNotin.Location = New System.Drawing.Point(14, 176)
        Me.cbPuestoNotin.Margin = New System.Windows.Forms.Padding(2)
        Me.cbPuestoNotin.Name = "cbPuestoNotin"
        Me.cbPuestoNotin.Size = New System.Drawing.Size(131, 23)
        Me.cbPuestoNotin.TabIndex = 4
        Me.cbPuestoNotin.Text = "Puesto Notin C"
        Me.cbPuestoNotin.UseVisualStyleBackColor = True
        '
        'btNotinKubo
        '
        Me.btNotinKubo.Font = New System.Drawing.Font("Bookman Old Style", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btNotinKubo.Location = New System.Drawing.Point(4, 29)
        Me.btNotinKubo.Margin = New System.Windows.Forms.Padding(2)
        Me.btNotinKubo.Name = "btNotinKubo"
        Me.btNotinKubo.Size = New System.Drawing.Size(152, 39)
        Me.btNotinKubo.TabIndex = 26
        Me.btNotinKubo.Text = "Notin + Kubo"
        Me.btNotinKubo.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox2.Controls.Add(Me.btNotinKubo)
        Me.GroupBox2.Font = New System.Drawing.Font("Bookman Old Style", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(377, 110)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupBox2.Size = New System.Drawing.Size(267, 290)
        Me.GroupBox2.TabIndex = 32
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Instalación"
        '
        'lbRuta
        '
        Me.lbRuta.AutoSize = True
        Me.lbRuta.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbRuta.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbRuta.Location = New System.Drawing.Point(16, 488)
        Me.lbRuta.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lbRuta.Name = "lbRuta"
        Me.lbRuta.Size = New System.Drawing.Size(63, 15)
        Me.lbRuta.TabIndex = 31
        Me.lbRuta.Text = "C:\NOTIN\"
        '
        'btDirDescargas
        '
        Me.btDirDescargas.Location = New System.Drawing.Point(16, 445)
        Me.btDirDescargas.Margin = New System.Windows.Forms.Padding(2)
        Me.btDirDescargas.Name = "btDirDescargas"
        Me.btDirDescargas.Size = New System.Drawing.Size(93, 39)
        Me.btDirDescargas.TabIndex = 30
        Me.btDirDescargas.Text = "Cambiar Ruta Descargas"
        Me.btDirDescargas.UseVisualStyleBackColor = True
        '
        'pbDescargas
        '
        Me.pbDescargas.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.pbDescargas.Location = New System.Drawing.Point(316, 466)
        Me.pbDescargas.Margin = New System.Windows.Forms.Padding(2)
        Me.pbDescargas.Name = "pbDescargas"
        Me.pbDescargas.Size = New System.Drawing.Size(75, 18)
        Me.pbDescargas.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.pbDescargas.TabIndex = 28
        Me.pbDescargas.Visible = False
        '
        'btDescargar
        '
        Me.btDescargar.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btDescargar.Location = New System.Drawing.Point(214, 445)
        Me.btDescargar.Margin = New System.Windows.Forms.Padding(2)
        Me.btDescargar.Name = "btDescargar"
        Me.btDescargar.Size = New System.Drawing.Size(99, 52)
        Me.btDescargar.TabIndex = 27
        Me.btDescargar.Text = "Comenzar Descargas"
        Me.btDescargar.UseVisualStyleBackColor = True
        '
        'InstaladorKubo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(688, 509)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.btSalir)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.lbRuta)
        Me.Controls.Add(Me.btDirDescargas)
        Me.Controls.Add(Me.pbDescargas)
        Me.Controls.Add(Me.btDescargar)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.MaximizeBox = False
        Me.Name = "InstaladorKubo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Instalador Kubo"
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lbUnidadF As Label
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents fbdDescarga As FolderBrowserDialog
    Friend WithEvents lb64bits As Label
    Friend WithEvents lbUsuario As Label
    Friend WithEvents lbSistemaO As Label
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents btSalir As Button
    Friend WithEvents btTodo As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents lbSoftwaredescargable As Label
    Friend WithEvents cbTerceros As CheckBox
    Friend WithEvents cbOffice2003 As CheckBox
    Friend WithEvents cbOffice2016 As CheckBox
    Friend WithEvents cbConfiguraNotin As CheckBox
    Friend WithEvents lblAncert As LinkLabel
    Friend WithEvents cbSferen As CheckBox
    Friend WithEvents cbRequisitos As CheckBox
    Friend WithEvents cbPasarelaSigno As CheckBox
    Friend WithEvents cbConfiguraWord2016 As CheckBox
    Friend WithEvents lbRequisitos As Label
    Friend WithEvents lbPaquetes As Label
    Friend WithEvents cbNemo As CheckBox
    Friend WithEvents cbPuestoNotin As CheckBox
    Friend WithEvents btNotinKubo As Button
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents lbRuta As Label
    Friend WithEvents btDirDescargas As Button
    Friend WithEvents pbDescargas As ProgressBar
    Friend WithEvents btDescargar As Button
    Friend WithEvents tlpUnidadF As ToolTip
    Friend WithEvents tlpDescargas As ToolTip
    Friend WithEvents cbOffice2016odt As CheckBox
    Friend WithEvents tlpOffice2016odt As ToolTip
End Class
