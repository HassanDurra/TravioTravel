$(document).ready(function () {
    let totalPassanger = 3;
    let passangerFormContainer = $('#tabpanepassangerContainer');
    let passangerLinkContainer = $('#tabpanepassangerLinks');
    let links = "";
    let passangerContent = "";
    for (let i = 1; i <= totalPassanger; i++) {
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
                    <input type="file" class="form-control" id="txt_name" name="txt_name"/>
                </div>
            </div><!-- end columns -->
        </div>    
        <div class="row">
            <div class="col-6 col-md-6">
                <div class="form-group">
                    <label>First Name</label>
                    <input type="text" class="form-control" id="txt_name" name="txt_name"/>
                </div>
            </div><!-- end columns -->
            
            <div class="col-6 col-md-6">
                <div class="form-group">
                    <label>Last Name</label>
                    <input type="text" class="form-control" id="txt_last_name" name="txt_last_name"/>
                </div>
            </div><!-- end columns -->
        </div><!-- end row -->
        
        <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <label>Date of Birth</label>
                    <input type="text" class="form-control dpd3" id="txt_dob" name="txt_dob"/>
                </div>
            </div><!-- end columns -->
            <div class="col-md-6">
                <div class="form-group">
                    <label>Age</label>
                    <input type="input" readonly class="form-control" placeholder="Select Date Of Birth" name="txt_dob"/>
                </div>
            </div><!-- end columns -->
            
            <div class="col-md-6">
                <div class="form-group">
                    <label>Passport Number</label>
                    <input type="text" class="form-control" name="txt_dob"/>
                </div>
            </div><!-- end columns -->
            
            <div class="col-md-6">
                <div class="form-group">
                    <label>Cnic Number (If Age is 18+)</label>
                    <input type="text" class="form-control" name="txt_dob"/>
                </div>
            </div><!-- end columns -->
            
            <div class="col-md-6">
                <div class="form-group">
                    <label>Country</label>
                    <select name=""class="form-control" id="">
                        <option value="">Select Country</option>
                    </select>
                </div>
            </div><!-- end columns -->
            
            <div class="col-md-6">
                <div class="form-group">
                    <label>City</label>
                    <select name=""class="form-control" id="">
                        <option value="">Select City</option>
                    </select>
                </div>
            </div><!-- end columns -->

        </div><!-- end row -->
        
        <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <label>Email Address</label>
                    <input type="email" class="form-control" id="txt_email" name="txt_email"/>
                </div>
            </div><!-- end columns -->
            
            <div class="col-md-6">
                <div class="form-group">
                    <label>Phone Number</label>
                    <input type="text" class="form-control" id="txt_phone" name="txt_phone"/>
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
                    <input type="file" class="form-control" id="txt_name" name="txt_name"/>
                </div>
            </div><!-- end columns -->
        </div>    
        <div class="row">
            <div class="col-6 col-md-6">
                <div class="form-group">
                    <label>First Name</label>
                    <input type="text" class="form-control" id="txt_name" name="txt_name"/>
                </div>
            </div><!-- end columns -->
            
            <div class="col-6 col-md-6">
                <div class="form-group">
                    <label>Last Name</label>
                    <input type="text" class="form-control" id="txt_last_name" name="txt_last_name"/>
                </div>
            </div><!-- end columns -->
        </div><!-- end row -->
        
        <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <label>Date of Birth</label>
                    <input type="text" class="form-control dpd3" id="txt_dob" name="txt_dob"/>
                </div>
            </div><!-- end columns -->
            <div class="col-md-6">
                <div class="form-group">
                    <label>Age</label>
                    <input type="input" readonly class="form-control" placeholder="Select Date Of Birth" name="txt_dob"/>
                </div>
            </div><!-- end columns -->
            
            <div class="col-md-6">
                <div class="form-group">
                    <label>Passport Number</label>
                    <input type="text" class="form-control" name="txt_dob"/>
                </div>
            </div><!-- end columns -->
            
            <div class="col-md-6">
                <div class="form-group">
                    <label>Cnic Number (If Age is 18+)</label>
                    <input type="text" class="form-control" name="txt_dob"/>
                </div>
            </div><!-- end columns -->
            
            <div class="col-md-6">
                <div class="form-group">
                    <label>Country</label>
                    <select name=""class="form-control" id="">
                        <option value="">Select Country</option>
                    </select>
                </div>
            </div><!-- end columns -->
            
            <div class="col-md-6">
                <div class="form-group">
                    <label>City</label>
                    <select name=""class="form-control" id="">
                        <option value="">Select City</option>
                    </select>
                </div>
            </div><!-- end columns -->

        </div><!-- end row -->
        
        <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <label>Email Address</label>
                    <input type="email" class="form-control" id="txt_email" name="txt_email"/>
                </div>
            </div><!-- end columns -->
            
            <div class="col-md-6">
                <div class="form-group">
                    <label>Phone Number</label>
                    <input type="text" class="form-control" id="txt_phone" name="txt_phone"/>
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
