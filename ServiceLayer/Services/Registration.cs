using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic;
using CoreEntities.Domain;
using CoreEntities.ViewModels;
using System.Web;
using RepositoryLayer;
using System.Transactions;
using ServiceLayer.Interfaces;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;
namespace ServiceLayer
{
    public class Registration : IRegistration
    {
        private UnitOfWork unitOfWork { get; set; }

        public Registration(UnitOfWork _UnitOfWork)
        {
            unitOfWork = _UnitOfWork;
        }

        public bool AddUser(APiRegisterViewModel model)
        {
            vCIOPRoEntities context = new vCIOPRoEntities();
            bool flag = false;

            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    Guid UniqueTenantId = System.Guid.NewGuid();
                    User TntDtl = new User()
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Password = model.Password,
                        UserName = model.EmailId,
                        UserType = 1,
                        LastLoginIP ="wqee",
                        UserID=12,
                        ProfilePic="rewr"
                    };
                    unitOfWork.GetRepositoryInstance<User>().Insert(TntDtl);
                    unitOfWork.Save();
                    Tenant TntDt2 = new Tenant()
                    {
                        //EmailId = model.EmailId,
                        PlanId = 1,
                        CompanyName=model.CompanyName,
                        SubDomain=model.SubDomain,
                        Status=1,
                        CompanyLogo="dfgd",
                        StripeCustId="hgfhy",
                        StripeChargeId="gftyh",
                        StripeBalanceTransactionId="hdtrt"
                    };
                    unitOfWork.GetRepositoryInstance<Tenant>().Insert(TntDt2);
                    unitOfWork.Save();

                    TenantBillingInfo TntDt3 = new TenantBillingInfo()
                    {
                        // BillingId=model.BillingId,
                        BillingEmail = model.BillingEmail,
                        Phone = model.Phone,
                        Address1 = model.Address1,
                        Address2 = model.Address2,
                        CountryId = 1,
                        StateId = 2,
                        City = model.City,
                        ZipCode = model.ZipCode
                    };
                    unitOfWork.GetRepositoryInstance<TenantBillingInfo>().Insert(TntDt3);
                    unitOfWork.Save();

                    TenantCardInfo TntDt4 = new TenantCardInfo()
                    {
                        // CreditId=model.CreditId,
                        FirstName=model.FirstName,
                        LastName=model.LastName,
                        CardNo = model.CardNo,
                        SecurityCode = model.SecurityCode,
                        ExpiryDate = model.ExpiryDate,
                        CardType=model.CardType,
                        Active=true
                    };
                    unitOfWork.GetRepositoryInstance<TenantCardInfo>().Insert(TntDt4);

                    unitOfWork.Save();
                    dbContextTransaction.Commit();
                    flag = true;
                }

                    catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    throw ex;
                }
            }
            return true;
        }

   
    }
}
