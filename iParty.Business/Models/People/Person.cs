using iParty.Business.Models.Addresses;
using System;
using System.Collections.Generic;

namespace iParty.Business.Models.People
{
    public class Person: Entity
    {
        public Person() : base() { }
        public Person(object user, string name, string document, object photo, SupplierOrCustomer supplierOrCustomer, Customer customerInfo, Supplier supplierInfo, List<Address> addresses, List<Phone> phones)
        {
            User = user;
            Name = name;
            Document = document;
            Photo = photo;
            SupplierOrCustomer = supplierOrCustomer;
            CustomerInfo = customerInfo;
            SupplierInfo = supplierInfo;
            Addresses = addresses;
            Phones = phones;
        }
        public object User { get; private set; }        
        public string Name { get; private set; }        
        public string Document { get; private set; }        
        public object Photo { get; private set; }              
        public SupplierOrCustomer SupplierOrCustomer { get; private set; }        
        public Customer CustomerInfo { get; private set; }        
        public Supplier SupplierInfo { get; private set; }        
        public List<Address> Addresses { get; private set; }
        public List<Phone> Phones { get; private set; }
        public void ReplacePhone(Guid phoneId, Phone newPhone)
        {
            var currentPhone = Phones.Find(x => x.Id == phoneId);

            if (currentPhone == null)
                throw new Exception("Não foi possível localizar o telefone informado");

            var index = Phones.IndexOf(currentPhone);

            Phones.Remove(currentPhone);            

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
