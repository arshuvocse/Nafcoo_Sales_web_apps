using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using Library.DAL.InternalCls;
using Library.DAO.SInventory_Entities;

namespace Library.DAL.SInventory_DAL
{
    public class EmpGeneralInfoDAL
    {
        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        public bool SaveEmployeeInfo(EmpGeneralInfo aGeneralInfo)
        {
            string insertQuery = @"insert into tblEmpGeneralInfo (EmpInfoId,EmpMasterCode,EmpName,ShortName,FatherName,MotherName,Religion,Nationality,DateOfBirth,PlaceOfBirth,BloodGroup,Gender,AddressPresent,AddressPermanent,MedicalInformation,
                                   PhoneNo,CellNumber,Email,EmpImage,SignatureImage,MaritalStatus,NationalIdNo,RefName,RefAddress,RefCellNo,JoiningDate,DesignationId,Designation,DepartmentId,DeptName) 
            values (" + aGeneralInfo.EmpInfoId + ",'" + aGeneralInfo.EmpMasterCode + "','" + aGeneralInfo.EmpName + "','" + aGeneralInfo.ShortName + "','" + aGeneralInfo.FatherName + "','" + aGeneralInfo.MotherName + "','" + aGeneralInfo.Religion + "','" + aGeneralInfo.Nationality + "','" + aGeneralInfo.DateOfBirth + "','" + aGeneralInfo.PlaceOfBirth + "'," +
                                  "'" + aGeneralInfo.BloodGroup + "','" + aGeneralInfo.Gender + "','" + aGeneralInfo.AddressPresent + "','" + aGeneralInfo.AddressPermanent + "','" + aGeneralInfo.MedicalInformation + "','" + aGeneralInfo.PhoneNo + "','" + aGeneralInfo.CellNumber + "','" + aGeneralInfo.Email + "','" + aGeneralInfo.EmpImage + "'," +
                                  "'" + aGeneralInfo.SignatureImage + "','" + aGeneralInfo.MaritalStatus + "','" + aGeneralInfo.NationalIdNo + "','" + aGeneralInfo.ReferanceName + "','" + aGeneralInfo.ReferanceAddress + "','" + aGeneralInfo.ReferanceCellNo + "','" + aGeneralInfo.JoiningDate + "'," + aGeneralInfo.DesignationId + ",'" + aGeneralInfo.Designation + "'," + aGeneralInfo.DepartmentId + ",'" + aGeneralInfo.DeptName + "')";
            return aCommonInternalDal.SaveDataByInsertCommand(insertQuery, "SSIDB");
        }

        public DataTable LoadEmployeeView()
        {
            string query = @"select * from tblEmpGeneralInfo";
                           
                
            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }

        public DataTable LoadEmpGeneralInformation()
        {
            string query = @"SELECT * FROM tblEmpGeneralInfo ";
            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }


        public void LoadDesignationName(DropDownList ddl)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "select * from tblDesignation";
            aInternalDal.LoadDropDownValueWithoutDataBase(ddl, "DesigName", "DesignationId", queryStr);
        }

        public void LoadDepartmentName(DropDownList ddl)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "select * from tblDepartment";
            aInternalDal.LoadDropDownValueWithoutDataBase(ddl, "DeptName", "DeptId", queryStr);
        }

       
        public EmpGeneralInfo EmpInfoEditLoad(string employeeId)
        {
            string query = "select * from tblEmpGeneralInfo where EmpInfoId = '" + employeeId + "'";
            IDataReader dataReader = aCommonInternalDal.DataContainerDataReader(query, "SSIDB");
            EmpGeneralInfo aEmpGeneralInfo = new EmpGeneralInfo();
            if (dataReader != null)
            {
                while (dataReader.Read())
                {
                    aEmpGeneralInfo.EmpInfoId = Int32.Parse(dataReader["EmpInfoId"].ToString());
                    aEmpGeneralInfo.EmpMasterCode = dataReader["EmpMasterCode"].ToString();
                    aEmpGeneralInfo.EmpName = dataReader["EmpName"].ToString();
                    aEmpGeneralInfo.ShortName = dataReader["ShortName"].ToString();
                    aEmpGeneralInfo.FatherName = dataReader["FatherName"].ToString();
                    aEmpGeneralInfo.MotherName = dataReader["MotherName"].ToString();
                    aEmpGeneralInfo.Religion = dataReader["Religion"].ToString();
                    aEmpGeneralInfo.Nationality = dataReader["Nationality"].ToString();
                    aEmpGeneralInfo.DateOfBirth = dataReader["DateOfBirth"].ToString();
                    aEmpGeneralInfo.PlaceOfBirth = dataReader["PlaceOfBirth"].ToString();
                    aEmpGeneralInfo.BloodGroup = dataReader["BloodGroup"].ToString();
                    aEmpGeneralInfo.Gender = dataReader["Gender"].ToString();
                    aEmpGeneralInfo.AddressPresent = dataReader["AddressPresent"].ToString();
                    aEmpGeneralInfo.AddressPermanent = dataReader["AddressPermanent"].ToString();
                    aEmpGeneralInfo.MedicalInformation = dataReader["MedicalInformation"].ToString();
                    aEmpGeneralInfo.PhoneNo = dataReader["PhoneNo"].ToString();
                    aEmpGeneralInfo.CellNumber = dataReader["CellNumber"].ToString();
                    aEmpGeneralInfo.Email = dataReader["Email"].ToString();
                    aEmpGeneralInfo.MaritalStatus = dataReader["MaritalStatus"].ToString();
                    aEmpGeneralInfo.NationalIdNo = dataReader["NationalIdNo"].ToString();
                    aEmpGeneralInfo.ReferanceName = dataReader["RefName"].ToString();
                    aEmpGeneralInfo.ReferanceAddress = dataReader["RefAddress"].ToString();
                    aEmpGeneralInfo.ReferanceCellNo = dataReader["RefCellNo"].ToString();
                    //aEmpGeneralInfo.EmpImage = dataReader["EmpImage"].ToString();
                    //aEmpGeneralInfo.SignatureImage = dataReader["SignatureImage"].ToString();
                    aEmpGeneralInfo.DepartmentId = Int32.Parse(dataReader["DepartmentId"].ToString());
                    aEmpGeneralInfo.DeptName = (dataReader["DepartmentId"].ToString());
                    aEmpGeneralInfo.JoiningDate = Convert.ToDateTime(dataReader["JoiningDate"].ToString());
                    aEmpGeneralInfo.Designation = dataReader["Designation"].ToString();
                    aEmpGeneralInfo.DesignationId = Convert.ToInt32(dataReader["DesignationId"].ToString());
                }
            }
            return aEmpGeneralInfo;
        }

        public bool UpdateEmployeeInfo(EmpGeneralInfo aEmpGeneralInfo)
        {
            string query = @"UPDATE tblEmpGeneralInfo SET EmpMasterCode='" + aEmpGeneralInfo.EmpMasterCode + "',EmpName='" + aEmpGeneralInfo.EmpName + "',ShortName='" + aEmpGeneralInfo.ShortName + "',FatherName='" + aEmpGeneralInfo.FatherName + "',MotherName='" + aEmpGeneralInfo.MotherName + "',Religion='" + aEmpGeneralInfo.Religion + "',Nationality='" + aEmpGeneralInfo.Nationality + "'," +
                           "DateOfBirth='" + aEmpGeneralInfo.DateOfBirth + "',PlaceOfBirth='" + aEmpGeneralInfo.PlaceOfBirth + "',BloodGroup='" + aEmpGeneralInfo.BloodGroup + "',Gender='" + aEmpGeneralInfo.Gender + "',AddressPresent='" + aEmpGeneralInfo.AddressPresent + "',AddressPermanent='" + aEmpGeneralInfo.AddressPermanent + "',MedicalInformation='" + aEmpGeneralInfo.MedicalInformation + "'," +
                           "PhoneNo='" + aEmpGeneralInfo.PhoneNo + "',CellNumber='" + aEmpGeneralInfo.CellNumber + "',Email='" + aEmpGeneralInfo.Email + "',MaritalStatus='" + aEmpGeneralInfo.MaritalStatus + "',NationalIdNo='" + aEmpGeneralInfo.NationalIdNo + "',RefCellNo='" + aEmpGeneralInfo.ReferanceCellNo + "',RefAddress='" + aEmpGeneralInfo.ReferanceAddress + "',RefName='" + aEmpGeneralInfo.ReferanceName + "'," +
                           "JoiningDate='" + aEmpGeneralInfo.JoiningDate + "',DepartmentId='" + aEmpGeneralInfo.DepartmentId + "',Designation='" + aEmpGeneralInfo.Designation + "'EmpImage='" + aEmpGeneralInfo.EmpImage + "',SignatureImage='" + aEmpGeneralInfo.SignatureImage + "' WHERE EmpInfoId=" + aEmpGeneralInfo.EmpInfoId + "";
            return aCommonInternalDal.UpdateDataByUpdateCommand(query, "SSIDB");
        }


        public List<EmpGeneralInfo> ViewAllEmployee()
        {
            List<EmpGeneralInfo> allEmpGeneralInfoList = new List<EmpGeneralInfo>();
            string query = @"select * from tblEmpGeneralInfo";

            IDataReader dataReader = aCommonInternalDal.DataContainerDataReader(query, "SSIDB");

            while (dataReader.Read())
            {
                EmpGeneralInfo aGeneralInfo = new EmpGeneralInfo();
                aGeneralInfo.EmpInfoId = Int32.Parse(dataReader["EmpInfoId"].ToString());
                aGeneralInfo.EmpMasterCode = (dataReader["EmpMasterCode"].ToString());
                aGeneralInfo.EmpName = dataReader["EmpName"].ToString();
                allEmpGeneralInfoList.Add(aGeneralInfo);
            }

            return allEmpGeneralInfoList;
        }

        public List<EmpGeneralInfo> ViewEmpName(string employeeId)
        {
            List<EmpGeneralInfo> singleEmpNameList = ViewAllEmployee();
            List<EmpGeneralInfo> singleEmpName = (from EmpGeneralInfo aGeneralInfo in singleEmpNameList
                                                  where aGeneralInfo.EmpMasterCode == employeeId
                                                  select aGeneralInfo).ToList();
            return singleEmpName;
        }

        
    }
}
