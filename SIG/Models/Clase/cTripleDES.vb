﻿Imports System.IO
Imports System.Security.Cryptography

Public Class cTripleDES

    Private m_des As New TripleDESCryptoServiceProvider
    Private m_utf8 As New UTF8Encoding
    Private m_key() As Byte
    Private m_iv() As Byte

    Public Sub New(ByVal key() As Byte, ByVal iv() As Byte)
        Me.m_key = key
        Me.m_iv = iv
    End Sub

    Public Function Encrypt(ByVal input() As Byte) As Byte()
        Return Transform(input, m_des.CreateEncryptor(m_key, m_iv))
    End Function

    Public Function Decrypt(ByVal input() As Byte) As Byte()
        Return Transform(input, m_des.CreateDecryptor(m_key, m_iv))
    End Function

    Public Function Encrypt(ByVal text As String) As String
        Dim input() As Byte = m_utf8.GetBytes(text)
        Dim output() As Byte = Transform(input,
                        m_des.CreateEncryptor(m_key, m_iv))
        Return Convert.ToBase64String(output)
    End Function

    Public Function Decrypt(ByVal text As String) As String
        Dim input() As Byte = Convert.FromBase64String(text)
        Dim output() As Byte = Transform(input,
                         m_des.CreateDecryptor(m_key, m_iv))
        Return m_utf8.GetString(output)
    End Function

    Private Function Transform(ByVal input() As Byte,
        ByVal CryptoTransform As ICryptoTransform) As Byte()

        Dim memStream As MemoryStream = New MemoryStream
        Dim cryptStream As CryptoStream = New _
            CryptoStream(memStream, CryptoTransform,
            CryptoStreamMode.Write)

        cryptStream.Write(input, 0, input.Length)
        cryptStream.FlushFinalBlock()

        memStream.Position = 0
        Dim result(CType(memStream.Length - 1, System.Int32)) As Byte
        memStream.Read(result, 0, CType(result.Length, System.Int32))

        memStream.Close()
        cryptStream.Close()

        Return result
    End Function

End Class