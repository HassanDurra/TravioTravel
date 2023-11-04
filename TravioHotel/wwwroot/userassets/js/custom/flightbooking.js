$(document).ready(function () {
 
    let passangerFormContainer = $('#tabpanepassangerContainer');
    let passangerLinkContainer = $('#tabpanepassangerLinks');
    let links = "";
    let passangerContent = "";
    for (let i = 1; i <= passengers ; i++) {
        if (i === 1) {
            links += `
        <li class="nav-item"><a class="nav-link active" href="#tab-passanger-${i}" data-bs-toggle="tab"> Passanger ${i}</a></li>                                       
        `;
            passangerContent += `
       <div id="tab-passanger-${i}" class="tab-pane in active">
    
   <div class="personal-info">

        <div class="row">
            <div class="col-12 col-md-12">
                <div class="form-group">
                    <label>Person Image</label>
                                  <input type="file" class="form-control" name="passanger_image[]" multiple/>

                </div>
            </div><!-- end columns -->
        </div>    
        <div class="row">
            <div class="col-6 col-md-6">
                <div class="form-group">
                    <label>First Name</label>
                    <input type="text" class="form-control" id="txt_name"  placeholder="Enter First Name" name="first_name[]"/>
                </div>
            </div><!-- end columns -->
            
            <div class="col-6 col-md-6">
                <div class="form-group">
                    <label>Last Name</label>
                    <input type="text" class="form-control" id="txt_last_name"  placeholder="Enter Last Name" name="last_name[]"/>
                </div>
            </div><!-- end columns -->
        </div><!-- end row -->
        
        <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <label>Date of Birth</label>
                    <input type="text" class="form-control dpd3" id="txt_dob"  placeholder="Enter Date of birth" name="date_of_birth[]"/>
                </div>
            </div><!-- end columns -->
            <div class="col-md-6">
                <div class="form-group">
                    <label>Age</label>
                    <input type="input" readonly class="form-control" placeholder="Enter Age" name="age[]"/>
                </div>
            </div><!-- end columns -->
            
            <div class="col-md-6">
                <div class="form-group">
                    <label>Passport Number</label>
                    <input type="text" class="form-control"  placeholder="Enter Passport number" name="passport_number[]"/>
                </div>
            </div><!-- end columns -->
            
            <div class="col-md-6">
                <div class="form-group">
                    <label>Cnic Number (If Age is 18+)</label>
                    <input type="text" class="form-control"  placeholder="Enter Cnic Number" name="cnic_number[]"/>
                </div>
            </div><!-- end columns -->
            
            <div class="col-md-6">
                <div class="form-group">
                    <label>Country</label>
                 <input type="text" class="form-control" placeholder="Enter Country Name" name="country_name[]"/>
                </div>
            </div><!-- end columns -->
            
            <div class="col-md-6">
                <div class="form-group">
                    <label>City</label>
                 <input type="text" class="form-control" placeholder="Enter City Name" name="city[]"/>
                </div>
            </div><!-- end columns -->

        </div><!-- end row -->
        
        <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <label>Email Address</label>
                    <input type="email" class="form-control"  placeholder="Enter Email address" id="txt_email" name="email[]"/>
                </div>
            </div><!-- end columns -->
            
            <div class="col-md-6">
                <div class="form-group">
                    <label>Phone Number</label>
                    <input type="text" class="form-control"  placeholder="Enter Phone Number" id="txt_phone" name="phone_number[]"/>
                </div>
            </div><!-- end columns -->
        </div><!-- end row -->
    
  
    </div><!-- end personal-info -->
    
</div>
`;
        }
        else {
            links += `
        <li class="nav-item"><a class="nav-link" href="#tab-passanger-${i}" data-bs-toggle="tab"> Passanger ${i}</a></li>                                       
        `;
            passangerContent += `
       <div id="tab-passanger-${i}" class="tab-pane">
    
    <div class="personal-info">

        <div class="row">
            <div class="col-12 col-md-12">
                <div class="form-group">
                    <label>Person Image</label>
                    <input type="file" class="form-control" name="passanger_image[]" multiple/>
                </div>
            </div><!-- end columns -->
        </div>    
        <div class="row">
            <div class="col-6 col-md-6">
                <div class="form-group">
                    <label>First Name</label>
                    <input type="text" class="form-control" id="txt_name"  placeholder="Enter First Name" name="first_name[]"/>
                </div>
            </div><!-- end columns -->
            
            <div class="col-6 col-md-6">
                <div class="form-group">
                    <label>Last Name</label>
                    <input type="text" class="form-control" id="txt_last_name"  placeholder="Enter Last Name" name="last_name[]"/>
                </div>
            </div><!-- end columns -->
        </div><!-- end row -->
        
        <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <label>Date of Birth</label>
                    <input type="text" class="form-control dpd3" id="txt_dob"  placeholder="Enter Date of birth" name="date_of_birth[]"/>
                </div>
            </div><!-- end columns -->
            <div class="col-md-6">
                <div class="form-group">
                    <label>Age</label>
                    <input type="input" readonly class="form-control" placeholder="Enter Age" name="age[]"/>
                </div>
            </div><!-- end columns -->
            
            <div class="col-md-6">
                <div class="form-group">
                    <label>Passport Number</label>
                    <input type="text" class="form-control"  placeholder="Enter Passport number" name="passport_number[]"/>
                </div>
            </div><!-- end columns -->
            
            <div class="col-md-6">
                <div class="form-group">
                    <label>Cnic Number (If Age is 18+)</label>
                    <input type="text" class="form-control"  placeholder="Enter Cnic Number" name="cnic_number[]"/>
                </div>
            </div><!-- end columns -->
            
            <div class="col-md-6">
                <div class="form-group">
                    <label>Country</label>
                 <input type="text" class="form-control" placeholder="Enter Country Name" name="country_name[]"/>
                </div>
            </div><!-- end columns -->
            
            <div class="col-md-6">
                <div class="form-group">
                    <label>City</label>
                 <input type="text" class="form-control" placeholder="Enter City Name" name="city[]"/>
                </div>
            </div><!-- end columns -->

        </div><!-- end row -->
        
        <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <label>Email Address</label>
                    <input type="email" class="form-control"  placeholder="Enter Email address" id="txt_email" name="email[]"/>
                </div>
            </div><!-- end columns -->
            
            <div class="col-md-6">
                <div class="form-group">
                    <label>Phone Number</label>
                    <input type="text" class="form-control"  placeholder="Enter Phone Number" id="txt_phone" name="phone_number[]"/>
                </div>
            </div><!-- end columns -->
        </div><!-- end row -->

    </div><!-- end personal-info -->
    
</div>
`;
        }


    }
    passangerLinkContainer.html(links);
    passangerFormContainer.html(passangerContent);
});
