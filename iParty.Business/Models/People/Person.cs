using iParty.Business.Models.Addresses;
using System;
using System.Collections.Generic;

namespace iParty.Business.Models.People
{
    public class Person: Entity
    {
        public object User { get; set; }
        
        public string Name { get; set; }
        
        public string Document { get; set; }
        
        public object Photo { get; set; }      
        
        public SupplierOrCustomer SupplierOrCustomer { get; set; }
        
        public Customer CustomerInfo { get; set; }
        
        public Supplier SupplierInfo { get; set; }
        
        public List<Address> Addresses { get; set; }

        public List<Phone> Phones { get; set; }

        public void ReplacePhone(Guid phoneId, Phone newPhone)
        {
            var currentPhone = Phones.Find(x => x.Id == phoneId);

            if (currentPhone == null)
                throw new Exception("Não foi possível localizar o telefone informado");

            var index = Phones.IndexOf(currentPhone);

            Phones.Remove(currentPhone);

            newPhone.Id = phoneId;

            Phones.Insert(index, newPhone);            
        }
       
        public void RemovePhone(Guid phoneId)
        {
            var currentPhone = Phones.Find(x => x.Id == phoneId);

            if (currentPhone == null)
                throw new Exception("Não foi possível localizar o telefone informado");

            var index = Phones.IndexOf(currentPhone);

            Phones.Remove(currentPhone);            
        }

        public void ReplaceAddress(Guid addressId, Address newAddress)
        {
            var currentAddress = Addresses.Find(x => x.Id == addressId);

            if (currentAddress == null)
                throw new Exception("Não foi possível localizar o endereço informado");

            var index = Addresses.IndexOf(currentAddress);

            Addresses.Remove(currentAddress);

            newAddress.Id = addressId;

            Addresses.Insert(index, newAddress);            
        }

        public void RemoveAddress(Guid addressId)
        {
            var currentAddress = Addresses.Find(x => x.Id == addressId);

            if (currentAddress == null)
                throw new Exception("Não foi possível localizar o endereço informado");

            var index = Addresses.IndexOf(currentAddress);

            Addresses.Remove(currentAddress);
        }

        public void RemovePaymentPlan(Guid paymentPlanId)
        {
            var currentPaymentPlan = SupplierInfo.PaymentPlans.Find(x => x.Id == paymentPlanId);

            if (currentPaymentPlan == null)
                throw new Exception("Não foi possível localizar o plano de pagamento informado");

            var index = SupplierInfo.PaymentPlans.IndexOf(currentPaymentPlan);

            SupplierInfo.PaymentPlans.Remove(currentPaymentPlan);            
        }
    }
}
