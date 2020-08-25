Imports System
Imports System.Runtime.InteropServices
Imports System.Net

Namespace TimeZoneInformation
    Public Class TimeZoneFunctionality
        ''' <summary>
        ''' [Win32 API call]
        ''' The GetTimeZoneInformation function retrieves the current time-zone parameters.
        ''' These parameters control the translations between Coordinated Universal Time (UTC)
        ''' and local time.
        ''' </summary>
        ''' <param name="lpTimeZoneInformation">[out] Pointer to a TIME_ZONE_INFORMATION structure to receive the current time-zone parameters.</param>
        ''' <returns>
        ''' If the function succeeds, the return value is one of the following values.
        ''' <list type="table">
        ''' <listheader>
        ''' <term>Return code/value</term>
        ''' <description>Description</description>
        ''' </listheader>
        ''' <item>
        ''' <term>TIME_ZONE_ID_UNKNOWN == 0</term>
        ''' <description>
        ''' The system cannot determine the current time zone. This error is also returned if you call the SetTimeZoneInformation function and supply the bias values but no transition dates.
        ''' This value is returned if daylight saving time is not used in the current time zone, because there are no transition dates.
        ''' </description>
        ''' </item>
        ''' <item>
        ''' <term>TIME_ZONE_ID_STANDARD == 1</term>
        ''' <description>
        ''' The system is operating in the range covered by the StandardDate member of the TIME_ZONE_INFORMATION structure.
        ''' </description>
        ''' </item>
        ''' <item>
        ''' <term>TIME_ZONE_ID_DAYLIGHT == 2</term>
        ''' <description>
        ''' The system is operating in the range covered by the DaylightDate member of the TIME_ZONE_INFORMATION structure.
        ''' </description>
        ''' </item>
        ''' </list>
        ''' If the function fails, the return value is TIME_ZONE_ID_INVALID. To get extended error information, call GetLastError.
        ''' </returns>
        <DllImport("kernel32.dll", CharSet:=CharSet.Auto)> _
        Private Shared Function GetTimeZoneInformation(ByRef lpTimeZoneInformation As TimeZoneInformation) As Integer
        End Function

        ''' <summary>
        ''' [Win32 API call]
        ''' The SetTimeZoneInformation function sets the current time-zone parameters.
        ''' These parameters control translations from Coordinated Universal Time (UTC)
        ''' to local time.
        ''' </summary>
        ''' <param name="lpTimeZoneInformation">[in] Pointer to a TIME_ZONE_INFORMATION structure that contains the time-zone parameters to set.</param>
        ''' <returns>
        ''' If the function succeeds, the return value is nonzero.
        ''' If the function fails, the return value is zero. To get extended error information, call GetLastError.
        ''' </returns>
        <DllImport("kernel32.dll", CharSet:=CharSet.Auto)> _
        Private Shared Function SetTimeZoneInformation(<[In]()> ByRef lpTimeZoneInformation As TimeZoneInformation) As Boolean
        End Function

        ''' <summary>
        ''' The SystemTime structure represents a date and time using individual members
        ''' for the month, day, year, weekday, hour, minute, second, and millisecond.
        ''' </summary>
        <StructLayoutAttribute(LayoutKind.Sequential)> _
        Public Structure SystemTime
            Public year As Short
            Public month As Short
            Public dayOfWeek As Short
            Public day As Short
            Public hour As Short
            Public minute As Short
            Public second As Short
            Public milliseconds As Short
        End Structure

        ''' <summary>
        ''' The TimeZoneInformation structure specifies information specific to the time zone.
        ''' </summary>
        <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)> _
        Public Structure TimeZoneInformation
            ''' <summary>
            ''' Current bias for local time translation on this computer, in minutes. The bias is the difference, in minutes, between Coordinated Universal Time (UTC) and local time. All translations between UTC and local time are based on the following formula:
            ''' <para>UTC = local time + bias</para>
            ''' <para>This member is required.</para>
            ''' </summary>
            Public bias As Integer
            ''' <summary>
            ''' Pointer to a null-terminated string associated with standard time. For example, "EST" could indicate Eastern Standard Time. The string will be returned unchanged by the GetTimeZoneInformation function. This string can be empty.
            ''' </summary>
            <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=32)> _
            Public standardName As String
            ''' <summary>
            ''' A SystemTime structure that contains a date and local time when the transition from daylight saving time to standard time occurs on this operating system. If the time zone does not support daylight saving time or if the caller needs to disable daylight saving time, the wMonth member in the SystemTime structure must be zero. If this date is specified, the DaylightDate value in the TimeZoneInformation structure must also be specified. Otherwise, the system assumes the time zone data is invalid and no changes will be applied.
            ''' <para>To select the correct day in the month, set the wYear member to zero, the wHour and wMinute members to the transition time, the wDayOfWeek member to the appropriate weekday, and the wDay member to indicate the occurence of the day of the week within the month (first through fifth).</para>
            ''' <para>Using this notation, specify the 2:00a.m. on the first Sunday in April as follows: wHour = 2, wMonth = 4, wDayOfWeek = 0, wDay = 1. Specify 2:00a.m. on the last Thursday in October as follows: wHour = 2, wMonth = 10, wDayOfWeek = 4, wDay = 5.</para>
            ''' </summary>
            Public standardDate As SystemTime
            ''' <summary>
            ''' Bias value to be used during local time translations that occur during standard time. This member is ignored if a value for the StandardDate member is not supplied.
            ''' <para>This value is added to the value of the Bias member to form the bias used during standard time. In most time zones, the value of this member is zero.</para>
            ''' </summary>
            Public standardBias As Integer
            ''' <summary>
            ''' Pointer to a null-terminated string associated with daylight saving time. For example, "PDT" could indicate Pacific Daylight Time. The string will be returned unchanged by the GetTimeZoneInformation function. This string can be empty.
            ''' </summary>
            <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=32)> _
            Public daylightName As String
            ''' <summary>
            ''' A SystemTime structure that contains a date and local time when the transition from standard time to daylight saving time occurs on this operating system. If the time zone does not support daylight saving time or if the caller needs to disable daylight saving time, the wMonth member in the SystemTime structure must be zero. If this date is specified, the StandardDate value in the TimeZoneInformation structure must also be specified. Otherwise, the system assumes the time zone data is invalid and no changes will be applied.
            ''' <para>To select the correct day in the month, set the wYear member to zero, the wHour and wMinute members to the transition time, the wDayOfWeek member to the appropriate weekday, and the wDay member to indicate the occurence of the day of the week within the month (first through fifth).</para>
            ''' </summary>
            Public daylightDate As SystemTime
            ''' <summary>
            ''' Bias value to be used during local time translations that occur during daylight saving time. This member is ignored if a value for the DaylightDate member is not supplied.
            ''' <para>This value is added to the value of the Bias member to form the bias used during daylight saving time. In most time zones, the value of this member is –60.</para>
            ''' </summary>
            Public daylightBias As Integer
        End Structure

        ''' <summary>
        ''' Sets new time-zone information for the local system.
        ''' </summary>
        ''' <param name="tzi">Struct containing the time-zone parameters to set.</param>
        Public Shared Sub SetTimeZone(ByVal tzi As TimeZoneInformation)
            ' set local system timezone
            SetTimeZoneInformation(tzi)
        End Sub

        ''' <summary>
        ''' Gets current timezone information for the local system.
        ''' </summary>
        ''' <returns>Struct containing the current time-zone parameters.</returns>
        Public Shared Function GetTimeZone() As TimeZoneInformation
            ' create struct instance
            Dim tzi As TimeZoneInformation = Nothing

            ' retrieve timezone info
            Dim currentTimeZone As Integer = GetTimeZoneInformation(tzi)

            Return tzi
        End Function

        ''' <summary>
        ''' Oversimplyfied method to test  the GetTimeZone functionality
        ''' </summary>
        ''' <param name="args">the usual stuff</param>
        Private Shared Sub Main(ByVal args As String())
            Dim timeZoneInformation As TimeZoneInformation = GetTimeZone()

            Return
        End Sub

        <DllImport("Kernel32.dll")> _
    Private Shared Sub GetLocalTime(ByRef time As SystemTime)

        End Sub
        <DllImport("Kernel32.dll")> _
        Private Shared Function SetLocalTime(ByRef time As SystemTime) As Boolean

        End Function
        Public Shared Function SetTime(ByVal ServerTime As Date) As Boolean
            Dim curTime As New SystemTime
            Dim cn As New SqlClient.SqlConnection
            Dim NamaServer() As String
            Dim LocalIP As Net.IPHostEntry = Dns.GetHostEntry(System.Net.Dns.GetHostName)
            Try
                curTime.year = ServerTime.Year
                curTime.month = ServerTime.Month
                curTime.dayOfWeek = ServerTime.DayOfWeek
                curTime.day = ServerTime.Day
                curTime.hour = ServerTime.Hour
                curTime.minute = ServerTime.Minute
                curTime.second = ServerTime.Second
                curTime.milliseconds = ServerTime.Millisecond

                cn.ConnectionString = conStr()
                NamaServer = cn.DataSource.ToString().Split("\")

                Select Case NamaServer(0).ToLower
                    Case "(local)", ".", "localhost", "127.0.0.1", System.Net.Dns.GetHostName.ToLower
                        Return True
                    Case Else
                        With Dns.GetHostByName(Dns.GetHostName())
                            For Each IP As IPAddress In .AddressList
                                If IP.ToString.ToLower = NamaServer(0).ToLower Then
                                    Return True
                                End If
                            Next
                        End With
                End Select

                Return SetLocalTime(curTime)

            Catch ex As Exception
                MessageBox.Show("Info Kesalahan : " & ex.Message, NamaAplikasi, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return False
            Finally
                cn.Dispose()
            End Try
        End Function
    End Class

    Public Class AdjustTokenPrivilegesFunctionality
        <StructLayout(LayoutKind.Sequential)> _
        Private Structure LUID
            Public LowPart As UInteger
            Public HighPart As Integer
        End Structure

        <StructLayout(LayoutKind.Sequential)> _
        Private Structure LUID_AND_ATTRIBUTES
            Public Luid As LUID
            Public Attributes As UInt32
        End Structure

        Private Structure TOKEN_PRIVILEGES
            Public PrivilegeCount As UInt32
            <MarshalAs(UnmanagedType.ByValArray, SizeConst:=1)> _
            Public Privileges As LUID_AND_ATTRIBUTES()
        End Structure

        Public Const SE_TIME_ZONE_NAME As String = "SeTimeZonePrivilege"
        Public Const TOKEN_ADJUST_PRIVILEGES As Integer = &H20
        Public Const TOKEN_QUERY As Integer = &H8
        Public Const SE_PRIVILEGE_ENABLED As Integer = &H2

        <DllImport("advapi32.dll", SetLastError:=True)> _
        Private Shared Function AdjustTokenPrivileges(ByVal TokenHandle As IntPtr, ByVal DisableAllPrivileges As Boolean, ByRef NewState As TOKEN_PRIVILEGES, ByVal Zero As UInt32, ByVal Null1 As IntPtr, ByVal Null2 As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        <DllImport("advapi32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
        Private Shared Function OpenProcessToken(ByVal ProcessHandle As Integer, ByVal DesiredAccess As Integer, ByRef tokenhandle As IntPtr) As Integer
        End Function

        <DllImport("kernel32.dll", CharSet:=CharSet.Auto)> _
        Private Shared Function GetCurrentProcess() As Integer
        End Function

        <DllImport("advapi32.dll", CharSet:=CharSet.Auto)> _
        Private Shared Function LookupPrivilegeValue(ByVal lpsystemname As String, ByVal lpname As String, <MarshalAs(UnmanagedType.Struct)> ByRef lpLuid As LUID) As Integer
        End Function

        Private Shared Sub EnableSetTimeZonePrivileges()
            ' We must add the set timezone privilege to the process token or SetTimeZoneInformation will fail
            Dim token As IntPtr
            Dim retval As Integer = OpenProcessToken(GetCurrentProcess(), TOKEN_ADJUST_PRIVILEGES Or TOKEN_QUERY, token)
            Trace.Assert(retval <> 0, [String].Format("OpenProcessToken failed. GetLastError: {0}", Marshal.GetLastWin32Error()))

            Dim luid As New LUID()
            retval = LookupPrivilegeValue(Nothing, SE_TIME_ZONE_NAME, luid)
            Trace.Assert(retval <> 0, [String].Format("LookupPrivilegeValue failed. GetLastError: {0}", Marshal.GetLastWin32Error()))

            Dim tokenPrivs As New TOKEN_PRIVILEGES()
            tokenPrivs.PrivilegeCount = 1
            tokenPrivs.Privileges = New LUID_AND_ATTRIBUTES(0) {}
            tokenPrivs.Privileges(0).Luid = luid
            tokenPrivs.Privileges(0).Attributes = SE_PRIVILEGE_ENABLED
            Trace.Assert(AdjustTokenPrivileges(token, False, tokenPrivs, 0, IntPtr.Zero, IntPtr.Zero), [String].Format("AdjustTokenPrivileges failed. GetLastError: {0}", Marshal.GetLastWin32Error()))
        End Sub
    End Class
End Namespace
