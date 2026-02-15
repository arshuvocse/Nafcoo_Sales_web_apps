namespace Library.DAO.DoctorModule_DAO
{
    public class DayPlan
    {
        public int Day { get; set; }
        public SessionPlan Morning { get; set; }
        public SessionPlan Evening { get; set; }
    }
}
