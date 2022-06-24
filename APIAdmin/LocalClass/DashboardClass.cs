using Class;
using Models.Enums;
using Models.Request;
using System.Security.Claims;

namespace APIAdmin.LocalClass
{
    public class DashboardClass
    {

        public static Dashboard_Request InformacionCompleta(ClaimsPrincipal _user, FilterDashboard_Request Filtro)
        {
            Filtro.Date_Start = GlobalClass.FechaInicialDeFiltro(Filtro.Date_Start);
            Filtro.Date_End = GlobalClass.FechaFinalDeFiltro(Filtro.Date_End);

            var filtroAnual = new FilterDashboard_Request()
            {
                Lapso = Filtro.Lapso,
                Date_Start = new DateTime(DateTime.Now.Year, 1, 1),
                Date_End = new DateTime(DateTime.Now.Year, 12, 31)
            };

            try
            {


                return null;
            }
            catch (Exception e)
            {
                Logs_ErrorsClass.NuevoLog(_user, "Could not load information for dashboard", SystemActionsEnum.LoadInformation, SystemTypesEnum.API, e, SystemErrorCodesEnum.Error);

                throw new Exception("Could not load information.");
            }
        }




        #region Get
        //private static List<LineData> DatosCVIG(ClaimsPrincipal _user, FilterDashboard_Request filterModel, List<Compra_Request> Compras, List<Venta_Request> Ventas)
        //{
        //    using var db = new UNG_Context();

        //    try
        //    {
        //        var listaTemp = (from ingreso in db.Ingresos_Gastos

        //                         join unidad in db.UnidadesDeNegocio
        //                         on ingreso.IDunidadDeNegocio equals unidad.ID
        //                         into cas
        //                         from unidad in cas.DefaultIfEmpty()

        //                         where ingreso.IDstatus == EstadosEnum.Activo

        //                         && (
        //                             (ingreso.Fecha_Emision >= filterModel.Fecha_Inicio.Value && ingreso.Fecha_Emision < filterModel.Fecha_Final.Value)
        //                                 ||
        //                             (ingreso.Fecha_Emision > filterModel.Fecha_Inicio.Value && ingreso.Fecha_Emision <= filterModel.Fecha_Final.Value)
        //                         )

        //                         select new LineData
        //                         {
        //                             IDunidadDeNegocio = unidad.ID,
        //                             Fecha = ingreso.Fecha_Emision.Date,
        //                             I = (!ingreso.EsGasto) ? ingreso.Monto : 0,
        //                             G = (ingreso.EsGasto) ? -ingreso.Monto : 0,
        //                         }).ToList();


        //        if (filterModel.IDunidadDeNegocio != null && filterModel.IDunidadDeNegocio.Count != 0)
        //        {
        //            try
        //            {
        //                var temp = listaTemp.Where(y => filterModel.IDunidadDeNegocio.Any(z => z == y.IDunidadDeNegocio)).ToList();
        //                listaTemp = temp;
        //            }
        //            catch (Exception)
        //            { }
        //        }

        //        List<LineData> lista = FiltrarListas(filterModel, listaTemp, Compras, Ventas);

        //        return lista;
        //    }
        //    catch (Exception e)
        //    {
        //        Logs_ErrorsClass.NuevoLog(_user, "No se pudo buscar los datos del gráfico por período", SystemActionsEnum.LoadInformation, SystemTypesEnum.API, e, SystemErrorCodesEnum.Error);

        //        throw new Exception("No se pudo buscar los datos del gráfico.");
        //    }
        //}
        #endregion Get

        #region Metodos
        //private static List<LineData> FiltrarListas(FilterDashboard_Request filterModel, List<LineData> _listaCVIGtemp, List<Compra_Request> compras, List<Venta_Request> ventas)
        //{
        //    DateTime fechaTemp = filterModel.Fecha_Inicio.Value;
        //    List<LineData> lista = new();

        //    if (filterModel.Lapso == LapsosParaFiltro_Dashboard.Dia)
        //    {
        //        do
        //        {
        //            lista.Add(new LineData
        //            {
        //                Fecha = fechaTemp,
        //                X = fechaTemp.ToString("dd/MM"),
        //                C = -compras.Where(x => x.Fecha_Emision == fechaTemp.Date).Sum(x => x.Total),
        //                V = ventas.Where(x => x.Fecha_Emision == fechaTemp.Date).Sum(x => x.Total),
        //                I = _listaCVIGtemp.Where(x => x.Fecha == fechaTemp.Date).Sum(x => x.I),
        //                G = _listaCVIGtemp.Where(x => x.Fecha == fechaTemp.Date).Sum(x => x.G)
        //            });

        //            if (fechaTemp.Date == filterModel.Fecha_Final.Value.Date)
        //                break;

        //            fechaTemp = fechaTemp.AddDays(1);
        //        } while (true);
        //    }

        //    else if (filterModel.Lapso == LapsosParaFiltro_Dashboard.Semana)
        //    {
        //        var dtf = DateTimeFormatInfo.CurrentInfo;

        //        int ultimaSemana = dtf.Calendar.GetWeekOfYear(filterModel.Fecha_Final.Value, dtf.CalendarWeekRule, DayOfWeek.Monday);
        //        do
        //        {
        //            decimal Ivalue = 0;
        //            decimal Gvalue = 0;
        //            decimal Cvalue = 0;
        //            decimal Vvalue = 0;
        //            var semanaTemp = dtf.Calendar.GetWeekOfYear(fechaTemp, dtf.CalendarWeekRule, DayOfWeek.Monday);

        //            while (semanaTemp == dtf.Calendar.GetWeekOfYear(fechaTemp, dtf.CalendarWeekRule, DayOfWeek.Monday))
        //            {
        //                Ivalue += _listaCVIGtemp.Where(x => x.Fecha == fechaTemp.Date).Sum(x => x.I);
        //                Gvalue += _listaCVIGtemp.Where(x => x.Fecha == fechaTemp.Date).Sum(x => x.G);
        //                Cvalue += -compras.Where(x => x.Fecha_Emision == fechaTemp.Date).Sum(x => x.Total);
        //                Vvalue += ventas.Where(x => x.Fecha_Emision == fechaTemp.Date).Sum(x => x.Total);

        //                if (fechaTemp.Date == filterModel.Fecha_Final.Value.Date)
        //                    break;

        //                fechaTemp = fechaTemp.AddDays(1);
        //            }

        //            var line = new LineData
        //            {
        //                Fecha = fechaTemp,
        //                I = Ivalue,
        //                G = Gvalue,
        //                C = Cvalue,
        //                V = Vvalue
        //            };

        //            int sem = GetWeekNumberOfMonth(fechaTemp);

        //            if (sem == 1 || sem == 3)
        //                line.X = sem + "ra/" + fechaTemp.ToString("MMM");
        //            else if (sem == 2)
        //                line.X = sem + "da/" + fechaTemp.ToString("MMM");
        //            else
        //                line.X = sem + "ta/" + fechaTemp.ToString("MMM");

        //            lista.Add(line);


        //            if (fechaTemp.Date == filterModel.Fecha_Final.Value.Date)
        //                break;

        //        } while (true);
        //    }

        //    else if (filterModel.Lapso == LapsosParaFiltro_Dashboard.Mes)
        //    {
        //        do
        //        {
        //            lista.Add(new LineData
        //            {
        //                Fecha = fechaTemp,
        //                X = fechaTemp.ToString("MMM/yyyy"),
        //                I = _listaCVIGtemp.Where(x => x.Fecha.Month == fechaTemp.Month && x.Fecha.Year == fechaTemp.Year).Sum(x => x.I),
        //                G = _listaCVIGtemp.Where(x => x.Fecha.Month == fechaTemp.Month && x.Fecha.Year == fechaTemp.Year).Sum(x => x.G),
        //                C = -compras.Where(x => x.Fecha_Emision.Month == fechaTemp.Month && x.Fecha_Emision.Year == fechaTemp.Year).Sum(x => x.Total),
        //                V = ventas.Where(x => x.Fecha_Emision.Month == fechaTemp.Month && x.Fecha_Emision.Year == fechaTemp.Year).Sum(x => x.Total),
        //            });

        //            if (fechaTemp.Year >= filterModel.Fecha_Final.Value.Year && fechaTemp.Month >= filterModel.Fecha_Final.Value.Month)
        //                break;

        //            fechaTemp = fechaTemp.AddMonths(1);
        //        } while (true);
        //    }

        //    else if (filterModel.Lapso == LapsosParaFiltro_Dashboard.Año)
        //    {
        //        do
        //        {
        //            lista.Add(new LineData
        //            {
        //                Fecha = fechaTemp,
        //                X = fechaTemp.ToString("yyyy"),
        //                I = _listaCVIGtemp.Where(x => x.Fecha.Year == fechaTemp.Year).Sum(x => x.I),
        //                G = _listaCVIGtemp.Where(x => x.Fecha.Year == fechaTemp.Year).Sum(x => x.G),
        //                C = -compras.Where(x => x.Fecha_Emision.Year == fechaTemp.Year).Sum(x => x.Total),
        //                V = ventas.Where(x => x.Fecha_Emision.Year == fechaTemp.Year).Sum(x => x.Total),
        //            });

        //            if (fechaTemp.Year >= filterModel.Fecha_Final.Value.Year)
        //                break;

        //            fechaTemp = fechaTemp.AddYears(1);
        //        } while (true);
        //    }

        //    return lista;
        //}

        //private static int GetWeekNumberOfMonth(DateTime date)
        //{
        //    date = date.Date;
        //    DateTime firstMonthDay = new(date.Year, date.Month, 1);
        //    DateTime firstMonthMonday = firstMonthDay.AddDays((DayOfWeek.Monday + 7 - firstMonthDay.DayOfWeek) % 7);
        //    if (firstMonthMonday > date)
        //    {
        //        firstMonthDay = firstMonthDay.AddMonths(-1);
        //        firstMonthMonday = firstMonthDay.AddDays((DayOfWeek.Monday + 7 - firstMonthDay.DayOfWeek) % 7);
        //    }
        //    return (date - firstMonthMonday).Days / 7 + 1;
        //}
        #endregion
    }
}
