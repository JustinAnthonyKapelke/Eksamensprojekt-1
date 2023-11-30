using Microsoft.Data.SqlClient.DataClassification;
using Nordlangelands_T�kkemand.Model;
using Nordlangelands_T�kkemand.ViewModel;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace UnitTest
{


    [TestClass]
    public class ThatchingTest
    {

        // CreateDelegate-metoden bruges som mock for ThatchingRepository.CreateMaterial i testen
        public ThatchingMaterial InitializeCreateDelegate(int materialID, string name, string description, int storageIndex, double price, int storageID)
        {
            return new ThatchingMaterial(materialID, name, description, storageIndex, price, storageID);
        }

        public ThatchingMaterial CreateDelegate(string name, string description, int storageIndex, double price, int storageID)
        {
            return new ThatchingMaterial(name, description, storageIndex, price, storageID);
        }

        ThatchingRepository thatchingRepo;
        ThatchingViewModel tvm;
        int MaterialID;

        [TestMethod]
        public void CreateAndInsertMaterialTest()
        {
            // ARRANGE
            thatchingRepo = new ThatchingRepository(CreateDelegate, InitializeCreateDelegate);
            tvm = new ThatchingViewModel();

            // ACT

            //Create material without ID
            //Insert material in database
            //Material is given ID in databas
            thatchingRepo.CreateMaterialInDatabase("Fr�gr�s", "Langkornet fr�gr�s", 1, 80, 1);
            thatchingRepo.InitializeMaterials();

            thatchingRepo.ReadLastAddedMaterialFromDatabase();

            // Skal have et ID
            ThatchingMaterial createdMaterial = thatchingRepo.ReadLastAddedMaterialFromDatabase();


            // ASSERT
            Assert.IsNotNull(createdMaterial);
            Assert.AreEqual("Fr�gr�s", createdMaterial.MaterialName);
            Assert.AreEqual("Langkornet fr�gr�s", createdMaterial.MaterialDescription);
            Assert.AreEqual(1, createdMaterial.MaterialStorageIndex);
            Assert.AreEqual(80, createdMaterial.MaterialPrice);
        }

        [TestCleanup]
        public void Cleanup()
        {
            ThatchingMaterial createdMaterial = thatchingRepo.ReadLastAddedMaterialFromDatabase();

            MaterialID = createdMaterial.MaterialID;

            thatchingRepo.DeleteMaterialFromDatabase(MaterialID);
        }

    }
}

