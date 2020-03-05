using System;
using EmaTours.BLL.DbObjects;
using EmaTours.BLL.Logic.Tables;
using EmaTours.Entities;
using System.Reflection;
using EmaTours.Utilities;
using EmaTours.BLL.Logic.Shared_Logic;

namespace EmaTours.BLL.UnitOfWork
{
    public class UnitOfWork
    {
        //Sample Data
        private static int LanguageId;
        private EMAToursEntities Context;
        private DateTime CreationDate;
        public UnitOfWork(int languageId,DateTime creationDate, EMAToursEntities context)
        {
            LanguageId = languageId;
            Context = context;
            CreationDate = creationDate;
        }
        #region Functions
        public bool Submit()
        {
            try

            {
                return Context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateEmaToursException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "EmaToursApplication-BLL");
                return false;
            }
        }
        #endregion

        private StoredFunctions storedFunctions;
        public StoredFunctions StoredFunctions
        {
            get
            {

                if (this.storedFunctions == null)
                {
                    this.storedFunctions = new StoredFunctions(LanguageId);
                }
                return storedFunctions;
            }
        }
        private ClientPickUpRequestsBLL clientPickUpRequestsBLL;
        public ClientPickUpRequestsBLL ClientPickUpRequestsBLL
        {
            get
            {

                if (this.clientPickUpRequestsBLL == null)
                {
                    this.clientPickUpRequestsBLL = new ClientPickUpRequestsBLL(Context, CreationDate);
                }
                return clientPickUpRequestsBLL;
            }
        }

        private ClientRequestsBLL clientRequestsBLL;
        public ClientRequestsBLL ClientRequestsBLL
        {
            get
            {

                if (this.clientRequestsBLL == null)
                {
                    this.clientRequestsBLL = new ClientRequestsBLL(Context, CreationDate);
                }
                return clientRequestsBLL;
            }
        }

        private ClientsBLL clientsBLL;
        public ClientsBLL ClientsBLL
        {
            get
            {

                if (this.clientsBLL == null)
                {
                    this.clientsBLL = new ClientsBLL(Context, CreationDate);
                }
                return clientsBLL;
            }
        }

        private ClientServicesRatingBLL clientServicesRatingBLL;
        public ClientServicesRatingBLL ClientServicesRatingBLL
        {
            get
            {

                if (this.clientServicesRatingBLL == null)
                {
                    this.clientServicesRatingBLL = new ClientServicesRatingBLL(Context, CreationDate);
                }
                return clientServicesRatingBLL;
            }
        }

        private ClientTripRequestsBLL clientTripRequestsBLL;
        public ClientTripRequestsBLL ClientTripRequestsBLL
        {
            get
            {

                if (this.clientTripRequestsBLL == null)
                {
                    this.clientTripRequestsBLL = new ClientTripRequestsBLL(Context, CreationDate);
                }
                return clientTripRequestsBLL;
            }
        }
        private ClientVisitsBLL clientVisitsBLL;
        public ClientVisitsBLL ClientVisitsBLL
        {
            get
            {

                if (this.clientVisitsBLL == null)
                {
                    this.clientVisitsBLL = new ClientVisitsBLL(Context, CreationDate);
                }
                return clientVisitsBLL;
            }
        }

        private ConfigurationsBLL configurationsBLL;
        public ConfigurationsBLL ConfigurationsBLL
        {
            get
            {

                if (this.configurationsBLL == null)
                {
                    this.configurationsBLL = new ConfigurationsBLL(Context, CreationDate);
                }
                return configurationsBLL;
            }
        }


        private CountriesBLL countriesBLL;
        public CountriesBLL CountriesBLL
        {
            get
            {

                if (this.countriesBLL == null)
                {
                    this.countriesBLL = new CountriesBLL(Context, CreationDate);
                }
                return countriesBLL;
            }
        }


        private CurrenciesBLL currenciesBLL;
        public CurrenciesBLL CurrenciesBLL
        {
            get
            {

                if (this.currenciesBLL == null)
                {
                    this.currenciesBLL = new CurrenciesBLL(Context, CreationDate);
                }
                return currenciesBLL;
            }
        }
        private EMAServicesBLL eMAServicesBLL;
        public EMAServicesBLL EMAServicesBLL
        {
            get
            {

                if (this.eMAServicesBLL == null)
                {
                    this.eMAServicesBLL = new EMAServicesBLL(Context, CreationDate);
                }
                return eMAServicesBLL;
            }
        }


        private EMAUserActionBLL eMAUserActionBLL;
        public EMAUserActionBLL EMAUserActionBLL
        {
            get
            {

                if (this.eMAUserActionBLL == null)
                {
                    this.eMAUserActionBLL = new EMAUserActionBLL(Context, CreationDate);
                }
                return eMAUserActionBLL;
            }
        }

        private EMAUsersBLL eMAUsersBLL;
        public EMAUsersBLL EMAUsersBLL
        {
            get
            {

                if (this.eMAUsersBLL == null)
                {
                    this.eMAUsersBLL = new EMAUsersBLL(Context, CreationDate);
                }
                return eMAUsersBLL;
            }
        }

        private EMAUserTypesBLL eMAUserTypesBLL;
        public EMAUserTypesBLL EMAUserTypesBLL
        {
            get
            {

                if (this.eMAUserTypesBLL == null)
                {
                    this.eMAUserTypesBLL = new EMAUserTypesBLL(Context, CreationDate);
                }
                return eMAUserTypesBLL;
            }
        }


        private LanguageBLL languageBLL;
        public LanguageBLL LanguageBLL
        {
            get
            {

                if (this.languageBLL == null)
                {
                    this.languageBLL = new LanguageBLL(Context, CreationDate);
                }
                return languageBLL;
            }
        }

        private NotificationDirectionBLL notificationDirectionBLL;
        public NotificationDirectionBLL NotificationDirectionBLL
        {
            get
            {

                if (this.notificationDirectionBLL == null)
                {
                    this.notificationDirectionBLL = new NotificationDirectionBLL(Context, CreationDate);
                }
                return notificationDirectionBLL;
            }
        }

        private NotificationMethodsBLL notificationMethodsBLL;
        public NotificationMethodsBLL NotificationMethodsBLL
        {
            get
            {

                if (this.notificationMethodsBLL == null)
                {
                    this.notificationMethodsBLL = new NotificationMethodsBLL(Context, CreationDate);
                }
                return notificationMethodsBLL;
            }
        }

        private NotificationsBLL notificationsBLL;
        public NotificationsBLL NotificationsBLL
        {
            get
            {

                if (this.notificationsBLL == null)
                {
                    this.notificationsBLL = new NotificationsBLL(Context, CreationDate);
                }
                return notificationsBLL;
            }
        }

        private OperatingCountriesBLL operatingCountriesBLL;
        public OperatingCountriesBLL OperatingCountriesBLL
        {
            get
            {

                if (this.operatingCountriesBLL == null)
                {
                    this.operatingCountriesBLL = new OperatingCountriesBLL(Context, CreationDate);
                }
                return operatingCountriesBLL;
            }
        }
        private OperatingLocationsBLL operatingLocationsBLL;
        public OperatingLocationsBLL OperatingLocationsBLL
        {
            get
            {

                if (this.operatingLocationsBLL == null)
                {
                    this.operatingLocationsBLL = new OperatingLocationsBLL(Context, CreationDate);
                }
                return operatingLocationsBLL;
            }
        }
        private StatusBLL statusBLL;
        public StatusBLL StatusBLL
        {
            get
            {

                if (this.statusBLL == null)
                {
                    this.statusBLL = new StatusBLL(Context, CreationDate);
                }
                return statusBLL;
            }
        }

        private TripPhotosBLL tripPhotosBLL;
        public TripPhotosBLL TripPhotosBLL
        {
            get
            {

                if (this.tripPhotosBLL == null)
                {
                    this.tripPhotosBLL = new TripPhotosBLL(Context, CreationDate);
                }
                return tripPhotosBLL;
            }
        }

        private TripsBLL tripsBLL;
        public TripsBLL TripsBLL
        {
            get
            {

                if (this.tripsBLL == null)
                {
                    this.tripsBLL = new TripsBLL(Context, CreationDate);
                }
                return tripsBLL;
            }
        }
        
         private InternalApplicationTextBLL internalApplicationTextBLL;
        public InternalApplicationTextBLL InternalApplicationTextBLL
        {
            get
            {

                if (this.internalApplicationTextBLL == null)
                {
                    this.internalApplicationTextBLL = new InternalApplicationTextBLL(Context, CreationDate);
                }
                return internalApplicationTextBLL;
            }
        }

        private SharedClientVisitsBLL sharedClientVisitsBLL;
        public SharedClientVisitsBLL SharedClientVisitsBLL
        {
            get
            {

                if (this.sharedClientVisitsBLL == null)
                {
                    this.sharedClientVisitsBLL = new SharedClientVisitsBLL(Context, CreationDate);
                }
                return sharedClientVisitsBLL;
            }
        }
        private SharedClientTripsBLL sharedClientTripsBLL;
        public SharedClientTripsBLL SharedClientTripsBLL
        {
            get
            {

                if (this.sharedClientTripsBLL == null)
                {
                    this.sharedClientTripsBLL = new SharedClientTripsBLL(Context, CreationDate);
                }
                return sharedClientTripsBLL;
            }
        }

        private ConfigurationBLL configurationBLL;
        public ConfigurationBLL ConfigurationBLL
        {
            get
            {

                if (this.configurationBLL == null)
                {
                    this.configurationBLL = new ConfigurationBLL(Context, CreationDate);
                }
                return configurationBLL;
            }
        }
        private SharedClientPickUp sharedClientPickUp;
        public SharedClientPickUp SharedClientPickUp
        {
            get
            {

                if (this.sharedClientPickUp == null)
                {
                    this.sharedClientPickUp = new SharedClientPickUp(Context, CreationDate);
                }
                return sharedClientPickUp;
            }
        }

        private HotelsBLL hotelsBLL;
        public HotelsBLL HotelsBLL
        {
            get
            {

                if (this.hotelsBLL == null)
                {
                    this.hotelsBLL = new HotelsBLL(Context, CreationDate);
                }
                return hotelsBLL;
            }
        }

    }
}