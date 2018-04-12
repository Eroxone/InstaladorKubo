Imports System.Runtime.InteropServices

Public Class CapturarVentanas
    <DllImport("user32.dll", EntryPoint:="FindWindow", SetLastError:=True, CharSet:=CharSet.Auto)>
    Private Shared Function FindWindowByCaption(ByVal zero As IntPtr, ByVal lpWindowName As String) As IntPtr
    End Function

    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
    Private Shared Function FindWindowEx(ByVal parentHandle As IntPtr, ByVal childAfter As IntPtr, ByVal lclassName As String, ByVal windowTitle As String) As IntPtr
    End Function

    <DllImport("user32.DLL", CharSet:=CharSet.Auto)>
    Public Shared Function SendMessage(ByVal hWnd As System.IntPtr, ByVal wMsg As Integer, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As Integer
    End Function

    Public Shared Function GetWindowHandle(ByVal tituloVentana As String) As IntPtr
        Dim handle As IntPtr = FindWindowByCaption(IntPtr.Zero, tituloVentana)
        Return handle
    End Function

    Public Shared Function GetWindowHandleChild(ByVal parentHandle As IntPtr, ByVal classNameChild As String, ByVal title As String) As IntPtr
        Dim h As IntPtr = FindWindowEx(parentHandle, IntPtr.Zero, classNameChild, title)
        Return h
    End Function
End Class
