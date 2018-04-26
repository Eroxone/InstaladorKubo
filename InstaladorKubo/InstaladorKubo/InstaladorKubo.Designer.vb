<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class InstaladorKubo
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
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
        Me.BtUac = New System.Windows.Forms.Button()
        Me.btJava = New System.Windows.Forms.Button()
        Me.btExcepJava = New System.Windows.Forms.Button()
        Me.btDirectivas = New System.Windows.Forms.Button()
        Me.btFramework = New System.Windows.Forms.Button()
        Me.btOdbc = New System.Windows.Forms.Button()
        Me.lbRuta = New System.Windows.Forms.Label()
        Me.btDescargar = New System.Windows.Forms.Button()
        Me.tlpUnidadF = New System.Windows.Forms.ToolTip(Me.components)
        Me.tlpDescargas = New System.Windows.Forms.ToolTip(Me.components)
        Me.tlpOffice2016odt = New System.Windows.Forms.ToolTip(Me.components)
        Me.tlpTerceros = New System.Windows.Forms.ToolTip(Me.components)
        Me.tlpNotinKubo = New System.Windows.Forms.ToolTip(Me.components)
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.lbProcesandoDescargas = New System.Windows.Forms.Label()
        Me.tlpTamaño = New System.Windows.Forms.ToolTip(Me.components)
        Me.lbInstalando = New System.Windows.Forms.Label()
        Me.tlpAncert = New System.Windows.Forms.ToolTip(Me.components)
        Me.tlpOffice2003 = New System.Windows.Forms.ToolTip(Me.components)
        Me.tlpOffice2016 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btDirDescargas = New System.Windows.Forms.Button()
        Me.TlpRutaDescargas = New System.Windows.Forms.ToolTip(Me.components)
        Me.TlpComenzarDescargas = New System.Windows.Forms.ToolTip(Me.components)
        Me.TlpJava = New System.Windows.Forms.ToolTip(Me.components)
        Me.TlpUac = New System.Windows.Forms.ToolTip(Me.components)
        Me.BtTraerdeF = New System.Windows.Forms.Button()
        Me.BtCopiarhaciaF = New System.Windows.Forms.Button()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.TlpPaquetesServidor = New System.Windows.Forms.ToolTip(Me.components)
        Me.TabGestion = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TlpCopiarServidor = New System.Windows.Forms.ToolTip(Me.components)
        Me.TlpTraerServidor = New System.Windows.Forms.ToolTip(Me.components)
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.TabGestion.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lbUnidadF
        '
        Me.lbUnidadF.AutoSize = True
        Me.lbUnidadF.Font = New System.Drawing.Font("Lucida Bright", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbUnidadF.ForeColor = System.Drawing.Color.DarkGray
        Me.lbUnidadF.Location = New System.Drawing.Point(4, 22)
        Me.lbUnidadF.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lbUnidadF.Name = "lbUnidadF"
        Me.lbUnidadF.Size = New System.Drawing.Size(100, 23)
        Me.lbUnidadF.TabIndex = 0
        Me.lbUnidadF.Text = "Unidad F"
        '
        'GroupBox4
        '
        Me.GroupBox4.BackColor = System.Drawing.Color.WhiteSmoke
        Me.GroupBox4.Controls.Add(Me.lbUnidadF)
        Me.GroupBox4.Font = New System.Drawing.Font("Lucida Bright", 12.0!, System.Drawing.FontStyle.Bold)
        Me.GroupBox4.Location = New System.Drawing.Point(356, 18)
        Me.GroupBox4.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Padding = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox4.Size = New System.Drawing.Size(205, 57)
        Me.GroupBox4.TabIndex = 35
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Unidad F"
        '
        'lb64bits
        '
        Me.lb64bits.AutoSize = True
        Me.lb64bits.Font = New System.Drawing.Font("Lucida Bright", 7.8!)
        Me.lb64bits.Location = New System.Drawing.Point(9, 37)
        Me.lb64bits.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lb64bits.Name = "lb64bits"
        Me.lb64bits.Size = New System.Drawing.Size(93, 14)
        Me.lb64bits.TabIndex = 26
        Me.lb64bits.Text = "Sistema x32/64"
        '
        'lbUsuario
        '
        Me.lbUsuario.AutoSize = True
        Me.lbUsuario.Font = New System.Drawing.Font("Lucida Bright", 7.8!)
        Me.lbUsuario.Location = New System.Drawing.Point(9, 53)
        Me.lbUsuario.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lbUsuario.Name = "lbUsuario"
        Me.lbUsuario.Size = New System.Drawing.Size(48, 14)
        Me.lbUsuario.TabIndex = 25
        Me.lbUsuario.Text = "Usuario"
        '
        'lbSistemaO
        '
        Me.lbSistemaO.AutoSize = True
        Me.lbSistemaO.Font = New System.Drawing.Font("Lucida Bright", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbSistemaO.Location = New System.Drawing.Point(9, 22)
        Me.lbSistemaO.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lbSistemaO.Name = "lbSistemaO"
        Me.lbSistemaO.Size = New System.Drawing.Size(106, 14)
        Me.lbSistemaO.TabIndex = 0
        Me.lbSistemaO.Text = "Sistema Operativo"
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.WhiteSmoke
        Me.GroupBox3.Controls.Add(Me.lb64bits)
        Me.GroupBox3.Controls.Add(Me.lbUsuario)
        Me.GroupBox3.Controls.Add(Me.lbSistemaO)
        Me.GroupBox3.Font = New System.Drawing.Font("Lucida Bright", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(30, 18)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox3.Size = New System.Drawing.Size(214, 79)
        Me.GroupBox3.TabIndex = 34
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Sistema"
        '
        'btSalir
        '
        Me.btSalir.Font = New System.Drawing.Font("Lucida Bright", 13.8!, System.Drawing.FontStyle.Bold)
        Me.btSalir.Location = New System.Drawing.Point(719, 514)
        Me.btSalir.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btSalir.Name = "btSalir"
        Me.btSalir.Size = New System.Drawing.Size(68, 32)
        Me.btSalir.TabIndex = 33
        Me.btSalir.Text = "Salir"
        Me.btSalir.UseVisualStyleBackColor = True
        '
        'btTodo
        '
        Me.btTodo.BackColor = System.Drawing.SystemColors.Control
        Me.btTodo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btTodo.Font = New System.Drawing.Font("Lucida Bright", 7.0!)
        Me.btTodo.Location = New System.Drawing.Point(224, 337)
        Me.btTodo.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btTodo.Name = "btTodo"
        Me.btTodo.Size = New System.Drawing.Size(85, 24)
        Me.btTodo.TabIndex = 19
        Me.btTodo.Text = "Marcar todos"
        Me.btTodo.UseVisualStyleBackColor = False
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
        Me.GroupBox1.Cursor = System.Windows.Forms.Cursors.Default
        Me.GroupBox1.Font = New System.Drawing.Font("Lucida Bright", 13.8!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle))
        Me.GroupBox1.Location = New System.Drawing.Point(16, 110)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox1.Size = New System.Drawing.Size(313, 365)
        Me.GroupBox1.TabIndex = 29
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Descargas"
        '
        'cbOffice2016odt
        '
        Me.cbOffice2016odt.AutoSize = True
        Me.cbOffice2016odt.Font = New System.Drawing.Font("Lucida Bright", 10.8!)
        Me.cbOffice2016odt.Location = New System.Drawing.Point(14, 105)
        Me.cbOffice2016odt.Name = "cbOffice2016odt"
        Me.cbOffice2016odt.Size = New System.Drawing.Size(153, 21)
        Me.cbOffice2016odt.TabIndex = 20
        Me.cbOffice2016odt.Text = "Office 2016 ODT"
        Me.cbOffice2016odt.UseVisualStyleBackColor = True
        '
        'lbSoftwaredescargable
        '
        Me.lbSoftwaredescargable.AutoSize = True
        Me.lbSoftwaredescargable.Font = New System.Drawing.Font("Lucida Bright", 10.2!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbSoftwaredescargable.Location = New System.Drawing.Point(13, 27)
        Me.lbSoftwaredescargable.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lbSoftwaredescargable.Name = "lbSoftwaredescargable"
        Me.lbSoftwaredescargable.Size = New System.Drawing.Size(127, 16)
        Me.lbSoftwaredescargable.TabIndex = 2
        Me.lbSoftwaredescargable.Text = "Aplicación Notin"
        '
        'cbTerceros
        '
        Me.cbTerceros.AutoSize = True
        Me.cbTerceros.Font = New System.Drawing.Font("Lucida Bright", 10.8!)
        Me.cbTerceros.Location = New System.Drawing.Point(14, 311)
        Me.cbTerceros.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.cbTerceros.Name = "cbTerceros"
        Me.cbTerceros.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cbTerceros.Size = New System.Drawing.Size(217, 21)
        Me.cbTerceros.TabIndex = 18
        Me.cbTerceros.Text = "Aplicaciones de Terceros"
        Me.cbTerceros.UseVisualStyleBackColor = True
        '
        'cbOffice2003
        '
        Me.cbOffice2003.AutoSize = True
        Me.cbOffice2003.BackColor = System.Drawing.SystemColors.Control
        Me.cbOffice2003.Font = New System.Drawing.Font("Lucida Bright", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbOffice2003.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cbOffice2003.Location = New System.Drawing.Point(14, 49)
        Me.cbOffice2003.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.cbOffice2003.Name = "cbOffice2003"
        Me.cbOffice2003.Size = New System.Drawing.Size(153, 21)
        Me.cbOffice2003.TabIndex = 0
        Me.cbOffice2003.Text = "Office 2003 ORK"
        Me.cbOffice2003.UseVisualStyleBackColor = False
        '
        'cbOffice2016
        '
        Me.cbOffice2016.AutoSize = True
        Me.cbOffice2016.Font = New System.Drawing.Font("Lucida Bright", 10.8!)
        Me.cbOffice2016.Location = New System.Drawing.Point(14, 81)
        Me.cbOffice2016.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.cbOffice2016.Name = "cbOffice2016"
        Me.cbOffice2016.Size = New System.Drawing.Size(114, 21)
        Me.cbOffice2016.TabIndex = 1
        Me.cbOffice2016.Text = "Office 2016"
        Me.cbOffice2016.UseVisualStyleBackColor = True
        '
        'cbConfiguraNotin
        '
        Me.cbConfiguraNotin.AutoSize = True
        Me.cbConfiguraNotin.Font = New System.Drawing.Font("Lucida Bright", 9.0!)
        Me.cbConfiguraNotin.Location = New System.Drawing.Point(180, 49)
        Me.cbConfiguraNotin.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.cbConfiguraNotin.Name = "cbConfiguraNotin"
        Me.cbConfiguraNotin.Size = New System.Drawing.Size(120, 19)
        Me.cbConfiguraNotin.TabIndex = 12
        Me.cbConfiguraNotin.Text = "Conf. Notin .Net"
        Me.cbConfiguraNotin.UseVisualStyleBackColor = True
        '
        'lblAncert
        '
        Me.lblAncert.AutoSize = True
        Me.lblAncert.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblAncert.Font = New System.Drawing.Font("Lucida Bright", 10.2!, System.Drawing.FontStyle.Bold)
        Me.lblAncert.LinkColor = System.Drawing.Color.Black
        Me.lblAncert.Location = New System.Drawing.Point(11, 229)
        Me.lblAncert.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblAncert.Name = "lblAncert"
        Me.lblAncert.Size = New System.Drawing.Size(127, 16)
        Me.lblAncert.TabIndex = 8
        Me.lblAncert.TabStop = True
        Me.lblAncert.Text = "Software Ancert"
        '
        'cbSferen
        '
        Me.cbSferen.AutoSize = True
        Me.cbSferen.Font = New System.Drawing.Font("Lucida Bright", 10.8!)
        Me.cbSferen.Location = New System.Drawing.Point(14, 248)
        Me.cbSferen.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.cbSferen.Name = "cbSferen"
        Me.cbSferen.Size = New System.Drawing.Size(74, 21)
        Me.cbSferen.TabIndex = 7
        Me.cbSferen.Text = "Sferen"
        Me.cbSferen.UseVisualStyleBackColor = True
        '
        'cbRequisitos
        '
        Me.cbRequisitos.AutoSize = True
        Me.cbRequisitos.Font = New System.Drawing.Font("Lucida Bright", 10.8!)
        Me.cbRequisitos.Location = New System.Drawing.Point(180, 171)
        Me.cbRequisitos.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.cbRequisitos.Name = "cbRequisitos"
        Me.cbRequisitos.Size = New System.Drawing.Size(136, 21)
        Me.cbRequisitos.TabIndex = 15
        Me.cbRequisitos.Text = "Pre-Requisitos"
        Me.cbRequisitos.UseVisualStyleBackColor = True
        '
        'cbPasarelaSigno
        '
        Me.cbPasarelaSigno.AutoSize = True
        Me.cbPasarelaSigno.Font = New System.Drawing.Font("Lucida Bright", 10.8!)
        Me.cbPasarelaSigno.Location = New System.Drawing.Point(14, 270)
        Me.cbPasarelaSigno.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.cbPasarelaSigno.Name = "cbPasarelaSigno"
        Me.cbPasarelaSigno.Size = New System.Drawing.Size(134, 21)
        Me.cbPasarelaSigno.TabIndex = 6
        Me.cbPasarelaSigno.Text = "Pasarela Signo"
        Me.cbPasarelaSigno.UseVisualStyleBackColor = True
        '
        'cbConfiguraWord2016
        '
        Me.cbConfiguraWord2016.AutoSize = True
        Me.cbConfiguraWord2016.Font = New System.Drawing.Font("Lucida Bright", 9.0!)
        Me.cbConfiguraWord2016.Location = New System.Drawing.Point(180, 84)
        Me.cbConfiguraWord2016.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.cbConfiguraWord2016.Name = "cbConfiguraWord2016"
        Me.cbConfiguraWord2016.Size = New System.Drawing.Size(126, 19)
        Me.cbConfiguraWord2016.TabIndex = 13
        Me.cbConfiguraWord2016.Text = "Conf. Word 2016"
        Me.cbConfiguraWord2016.UseVisualStyleBackColor = True
        '
        'lbRequisitos
        '
        Me.lbRequisitos.AutoSize = True
        Me.lbRequisitos.Font = New System.Drawing.Font("Lucida Bright", 10.2!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbRequisitos.Location = New System.Drawing.Point(177, 151)
        Me.lbRequisitos.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lbRequisitos.Name = "lbRequisitos"
        Me.lbRequisitos.Size = New System.Drawing.Size(108, 16)
        Me.lbRequisitos.TabIndex = 14
        Me.lbRequisitos.Text = "Software .Net"
        '
        'lbPaquetes
        '
        Me.lbPaquetes.AutoSize = True
        Me.lbPaquetes.Font = New System.Drawing.Font("Lucida Bright", 10.2!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbPaquetes.Location = New System.Drawing.Point(11, 151)
        Me.lbPaquetes.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lbPaquetes.Name = "lbPaquetes"
        Me.lbPaquetes.Size = New System.Drawing.Size(74, 16)
        Me.lbPaquetes.TabIndex = 5
        Me.lbPaquetes.Text = "Paquetes"
        '
        'cbNemo
        '
        Me.cbNemo.AutoSize = True
        Me.cbNemo.Font = New System.Drawing.Font("Lucida Bright", 10.8!)
        Me.cbNemo.Location = New System.Drawing.Point(14, 171)
        Me.cbNemo.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.cbNemo.Name = "cbNemo"
        Me.cbNemo.Size = New System.Drawing.Size(71, 21)
        Me.cbNemo.TabIndex = 3
        Me.cbNemo.Text = "Nemo"
        Me.cbNemo.UseVisualStyleBackColor = True
        '
        'cbPuestoNotin
        '
        Me.cbPuestoNotin.AutoSize = True
        Me.cbPuestoNotin.Font = New System.Drawing.Font("Lucida Bright", 10.8!)
        Me.cbPuestoNotin.Location = New System.Drawing.Point(14, 193)
        Me.cbPuestoNotin.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.cbPuestoNotin.Name = "cbPuestoNotin"
        Me.cbPuestoNotin.Size = New System.Drawing.Size(142, 21)
        Me.cbPuestoNotin.TabIndex = 4
        Me.cbPuestoNotin.Text = "Puesto Notin C"
        Me.cbPuestoNotin.UseVisualStyleBackColor = True
        '
        'btNotinKubo
        '
        Me.btNotinKubo.BackColor = System.Drawing.Color.Linen
        Me.btNotinKubo.Font = New System.Drawing.Font("Lucida Bright", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btNotinKubo.Location = New System.Drawing.Point(4, 29)
        Me.btNotinKubo.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btNotinKubo.Name = "btNotinKubo"
        Me.btNotinKubo.Size = New System.Drawing.Size(178, 39)
        Me.btNotinKubo.TabIndex = 26
        Me.btNotinKubo.Text = "NOTIN + KUBO"
        Me.btNotinKubo.UseVisualStyleBackColor = False
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox2.Controls.Add(Me.BtUac)
        Me.GroupBox2.Controls.Add(Me.btJava)
        Me.GroupBox2.Controls.Add(Me.btExcepJava)
        Me.GroupBox2.Controls.Add(Me.btDirectivas)
        Me.GroupBox2.Controls.Add(Me.btFramework)
        Me.GroupBox2.Controls.Add(Me.btOdbc)
        Me.GroupBox2.Controls.Add(Me.btNotinKubo)
        Me.GroupBox2.Font = New System.Drawing.Font("Lucida Bright", 13.8!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(350, 110)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox2.Size = New System.Drawing.Size(225, 361)
        Me.GroupBox2.TabIndex = 32
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Instalaciones"
        '
        'BtUac
        '
        Me.BtUac.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.BtUac.Font = New System.Drawing.Font("Lucida Bright", 12.0!, System.Drawing.FontStyle.Bold)
        Me.BtUac.Location = New System.Drawing.Point(4, 84)
        Me.BtUac.Name = "BtUac"
        Me.BtUac.Size = New System.Drawing.Size(167, 27)
        Me.BtUac.TabIndex = 32
        Me.BtUac.Text = "Excl. Def. - UAC"
        Me.BtUac.UseVisualStyleBackColor = True
        '
        'btJava
        '
        Me.btJava.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btJava.Font = New System.Drawing.Font("Lucida Bright", 12.0!, System.Drawing.FontStyle.Bold)
        Me.btJava.Location = New System.Drawing.Point(4, 289)
        Me.btJava.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btJava.Name = "btJava"
        Me.btJava.Size = New System.Drawing.Size(167, 27)
        Me.btJava.TabIndex = 31
        Me.btJava.Text = "Instalar JAVA"
        Me.btJava.UseVisualStyleBackColor = True
        '
        'btExcepJava
        '
        Me.btExcepJava.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btExcepJava.Font = New System.Drawing.Font("Lucida Bright", 12.0!, System.Drawing.FontStyle.Bold)
        Me.btExcepJava.Location = New System.Drawing.Point(4, 248)
        Me.btExcepJava.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btExcepJava.Name = "btExcepJava"
        Me.btExcepJava.Size = New System.Drawing.Size(167, 27)
        Me.btExcepJava.TabIndex = 30
        Me.btExcepJava.Text = "Excep. JAVA"
        Me.btExcepJava.UseVisualStyleBackColor = True
        '
        'btDirectivas
        '
        Me.btDirectivas.Enabled = False
        Me.btDirectivas.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btDirectivas.Font = New System.Drawing.Font("Lucida Bright", 12.0!, System.Drawing.FontStyle.Bold)
        Me.btDirectivas.Location = New System.Drawing.Point(4, 166)
        Me.btDirectivas.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btDirectivas.Name = "btDirectivas"
        Me.btDirectivas.Size = New System.Drawing.Size(167, 27)
        Me.btDirectivas.TabIndex = 29
        Me.btDirectivas.Text = "Conf. Directivas"
        Me.btDirectivas.UseVisualStyleBackColor = True
        '
        'btFramework
        '
        Me.btFramework.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btFramework.Font = New System.Drawing.Font("Lucida Bright", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btFramework.Location = New System.Drawing.Point(4, 126)
        Me.btFramework.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btFramework.Name = "btFramework"
        Me.btFramework.Size = New System.Drawing.Size(167, 27)
        Me.btFramework.TabIndex = 28
        Me.btFramework.Text = "Framework 3.5"
        Me.btFramework.UseVisualStyleBackColor = True
        '
        'btOdbc
        '
        Me.btOdbc.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btOdbc.Font = New System.Drawing.Font("Lucida Bright", 12.0!, System.Drawing.FontStyle.Bold)
        Me.btOdbc.Location = New System.Drawing.Point(4, 207)
        Me.btOdbc.Name = "btOdbc"
        Me.btOdbc.Size = New System.Drawing.Size(167, 27)
        Me.btOdbc.TabIndex = 27
        Me.btOdbc.Text = "ODBC NotinSQL"
        Me.btOdbc.UseVisualStyleBackColor = True
        '
        'lbRuta
        '
        Me.lbRuta.AutoSize = True
        Me.lbRuta.BackColor = System.Drawing.Color.SeaShell
        Me.lbRuta.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbRuta.Font = New System.Drawing.Font("Lucida Bright", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbRuta.Location = New System.Drawing.Point(13, 526)
        Me.lbRuta.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lbRuta.Name = "lbRuta"
        Me.lbRuta.Size = New System.Drawing.Size(70, 17)
        Me.lbRuta.TabIndex = 31
        Me.lbRuta.Text = "C:\NOTIN\"
        '
        'btDescargar
        '
        Me.btDescargar.BackColor = System.Drawing.Color.Azure
        Me.btDescargar.Font = New System.Drawing.Font("Lucida Bright", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btDescargar.Location = New System.Drawing.Point(208, 483)
        Me.btDescargar.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btDescargar.Name = "btDescargar"
        Me.btDescargar.Size = New System.Drawing.Size(121, 58)
        Me.btDescargar.TabIndex = 27
        Me.btDescargar.Text = "Comenzar Descargas"
        Me.btDescargar.UseVisualStyleBackColor = False
        '
        'tlpNotinKubo
        '
        Me.tlpNotinKubo.IsBalloon = True
        Me.tlpNotinKubo.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.NotifyIcon1.BalloonTipText = "Ejecutando InstaladorKubo"
        Me.NotifyIcon1.BalloonTipTitle = "Instalador Kubo"
        Me.NotifyIcon1.Icon = CType(resources.GetObject("NotifyIcon1.Icon"), System.Drawing.Icon)
        Me.NotifyIcon1.Text = "InstaladorKubo"
        Me.NotifyIcon1.Visible = True
        '
        'lbProcesandoDescargas
        '
        Me.lbProcesandoDescargas.AutoSize = True
        Me.lbProcesandoDescargas.Font = New System.Drawing.Font("Lucida Bright", 16.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbProcesandoDescargas.ForeColor = System.Drawing.Color.DarkRed
        Me.lbProcesandoDescargas.Location = New System.Drawing.Point(334, 483)
        Me.lbProcesandoDescargas.Name = "lbProcesandoDescargas"
        Me.lbProcesandoDescargas.Size = New System.Drawing.Size(283, 25)
        Me.lbProcesandoDescargas.TabIndex = 36
        Me.lbProcesandoDescargas.Text = "Procesando Descargas..."
        Me.lbProcesandoDescargas.Visible = False
        '
        'tlpTamaño
        '
        Me.tlpTamaño.IsBalloon = True
        Me.tlpTamaño.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.tlpTamaño.ToolTipTitle = "Tamaño Paquetes seleccionados"
        '
        'lbInstalando
        '
        Me.lbInstalando.AutoSize = True
        Me.lbInstalando.BackColor = System.Drawing.SystemColors.Control
        Me.lbInstalando.Font = New System.Drawing.Font("Lucida Bright", 16.2!, System.Drawing.FontStyle.Bold)
        Me.lbInstalando.ForeColor = System.Drawing.Color.DarkRed
        Me.lbInstalando.Location = New System.Drawing.Point(334, 483)
        Me.lbInstalando.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lbInstalando.Name = "lbInstalando"
        Me.lbInstalando.Size = New System.Drawing.Size(309, 25)
        Me.lbInstalando.TabIndex = 37
        Me.lbInstalando.Text = "Realizando Instalaciones..."
        Me.lbInstalando.Visible = False
        '
        'tlpAncert
        '
        Me.tlpAncert.IsBalloon = True
        Me.tlpAncert.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        '
        'tlpOffice2003
        '
        Me.tlpOffice2003.IsBalloon = True
        Me.tlpOffice2003.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        '
        'tlpOffice2016
        '
        Me.tlpOffice2016.IsBalloon = True
        '
        'btDirDescargas
        '
        Me.btDirDescargas.BackColor = System.Drawing.Color.Linen
        Me.btDirDescargas.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btDirDescargas.Font = New System.Drawing.Font("Lucida Bright", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btDirDescargas.Location = New System.Drawing.Point(13, 488)
        Me.btDirDescargas.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btDirDescargas.Name = "btDirDescargas"
        Me.btDirDescargas.Size = New System.Drawing.Size(114, 31)
        Me.btDirDescargas.TabIndex = 30
        Me.btDirDescargas.Text = "Ruta Descargas"
        Me.btDirDescargas.UseVisualStyleBackColor = False
        '
        'TlpRutaDescargas
        '
        Me.TlpRutaDescargas.IsBalloon = True
        Me.TlpRutaDescargas.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        '
        'TlpComenzarDescargas
        '
        Me.TlpComenzarDescargas.IsBalloon = True
        Me.TlpComenzarDescargas.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        '
        'TlpJava
        '
        Me.TlpJava.IsBalloon = True
        Me.TlpJava.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        '
        'TlpUac
        '
        Me.TlpUac.IsBalloon = True
        Me.TlpUac.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.TlpUac.ToolTipTitle = "Installer UAC"
        '
        'BtTraerdeF
        '
        Me.BtTraerdeF.Enabled = False
        Me.BtTraerdeF.Font = New System.Drawing.Font("Lucida Bright", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtTraerdeF.Location = New System.Drawing.Point(13, 56)
        Me.BtTraerdeF.Name = "BtTraerdeF"
        Me.BtTraerdeF.Size = New System.Drawing.Size(160, 36)
        Me.BtTraerdeF.TabIndex = 1
        Me.BtTraerdeF.Text = "Traer de Servidor"
        Me.BtTraerdeF.UseVisualStyleBackColor = True
        '
        'BtCopiarhaciaF
        '
        Me.BtCopiarhaciaF.Enabled = False
        Me.BtCopiarhaciaF.Font = New System.Drawing.Font("Lucida Bright", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtCopiarhaciaF.Location = New System.Drawing.Point(13, 8)
        Me.BtCopiarhaciaF.Name = "BtCopiarhaciaF"
        Me.BtCopiarhaciaF.Size = New System.Drawing.Size(160, 36)
        Me.BtCopiarhaciaF.TabIndex = 0
        Me.BtCopiarhaciaF.Text = "Copiar a Servidor"
        Me.BtCopiarhaciaF.UseVisualStyleBackColor = True
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(61, 4)
        '
        'TlpPaquetesServidor
        '
        Me.TlpPaquetesServidor.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        '
        'TabGestion
        '
        Me.TabGestion.Controls.Add(Me.TabPage1)
        Me.TabGestion.Controls.Add(Me.TabPage2)
        Me.TabGestion.Font = New System.Drawing.Font("Lucida Bright", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabGestion.Location = New System.Drawing.Point(600, 110)
        Me.TabGestion.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.TabGestion.Name = "TabGestion"
        Me.TabGestion.SelectedIndex = 0
        Me.TabGestion.Size = New System.Drawing.Size(184, 126)
        Me.TabGestion.TabIndex = 40
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.SystemColors.Control
        Me.TabPage1.Controls.Add(Me.BtTraerdeF)
        Me.TabPage1.Controls.Add(Me.BtCopiarhaciaF)
        Me.TabPage1.Location = New System.Drawing.Point(4, 24)
        Me.TabPage1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.TabPage1.Size = New System.Drawing.Size(176, 98)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Paquetes"
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.SystemColors.Control
        Me.TabPage2.ImeMode = System.Windows.Forms.ImeMode.Katakana
        Me.TabPage2.Location = New System.Drawing.Point(4, 24)
        Me.TabPage2.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.TabPage2.Size = New System.Drawing.Size(176, 98)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Registro"
        '
        'TlpCopiarServidor
        '
        Me.TlpCopiarServidor.IsBalloon = True
        Me.TlpCopiarServidor.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        '
        'TlpTraerServidor
        '
        Me.TlpTraerServidor.IsBalloon = True
        Me.TlpTraerServidor.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        '
        'InstaladorKubo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(793, 552)
        Me.Controls.Add(Me.TabGestion)
        Me.Controls.Add(Me.lbInstalando)
        Me.Controls.Add(Me.lbProcesandoDescargas)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.btSalir)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.lbRuta)
        Me.Controls.Add(Me.btDirDescargas)
        Me.Controls.Add(Me.btDescargar)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
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
        Me.TabGestion.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
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
    Friend WithEvents pbDescargas As ProgressBar
    Friend WithEvents btDescargar As Button
    Friend WithEvents tlpUnidadF As ToolTip
    Friend WithEvents tlpDescargas As ToolTip
    Friend WithEvents cbOffice2016odt As CheckBox
    Friend WithEvents tlpOffice2016odt As ToolTip
    Friend WithEvents tlpTerceros As ToolTip
    Friend WithEvents btOdbc As Button
    Friend WithEvents tlpNotinKubo As ToolTip
    Friend WithEvents NotifyIcon1 As NotifyIcon
    Friend WithEvents lbProcesandoDescargas As Label
    Friend WithEvents tlpTamaño As ToolTip
    Friend WithEvents lbInstalando As Label
    Friend WithEvents btExcepJava As Button
    Friend WithEvents btDirectivas As Button
    Friend WithEvents btFramework As Button
    Friend WithEvents tlpAncert As ToolTip
    Friend WithEvents tlpOffice2003 As ToolTip
    Friend WithEvents btJava As Button
    Friend WithEvents tlpOffice2016 As ToolTip
    Friend WithEvents btDirDescargas As Button
    Friend WithEvents TlpRutaDescargas As ToolTip
    Friend WithEvents TlpComenzarDescargas As ToolTip
    Friend WithEvents TlpJava As ToolTip
    Friend WithEvents BtUac As Button
    Friend WithEvents TlpUac As ToolTip
    Friend WithEvents BtTraerdeF As Button
    Friend WithEvents BtCopiarhaciaF As Button
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents TlpPaquetesServidor As ToolTip
    Friend WithEvents TabGestion As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents TlpCopiarServidor As ToolTip
    Friend WithEvents TlpTraerServidor As ToolTip
End Class
