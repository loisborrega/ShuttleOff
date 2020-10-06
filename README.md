### Step 1:
Comment the **line** 
```sh
private static string connString = ConfigurationManager.ConnectionStrings["Almer_SODB"]?.ConnectionString;
```
and comment your **Database** code in there.

Located in `ShuttleOffServiceAjax/Service1.svc.cs`

### Step 2:
**Right click** in the `ShuttleOffServiceAjax` solution in your Visual Studio and **click** the `Open Folder in File Explorer`.

### Step 3:
In the **File Explorer**, Go to the `ShuttleOff` Folder and copy the `ShuttleOffServiceAjax` folder.

### Step 4:
Go to **LocalDisk(C:) -> inetpub -> wwwroot** and copy your the folder in that location.

### Step 5:
Open your `IIS manager` and go to **Sites** -> **Default Web Site** and you can see your folder in there.

### Step 6:
**Right Click** on the folder in the IIS manager and select "Convert to application" 

### Step 7:
Select **OK** (Dont need to change anything).

### Step 8:
Go to `Content View` on the lower part of the window beside the `Feature View`

### Step 9:
Find the `Service1.svc` and **click** on it.  **NOTE: (DONT DOUBLE CLICK)**

### Step 10:
In the `Actions` panel located in the right of the window click `Browse`

**NOTE: (dapat lumabas yung svc na file sa browser hinde dapat ERROR nakasulat)**

### Step 11:
If no error in the svc file, **go** to go to the **Visual Studio 2017** and click the `index.js` file

### Step 12:
Find the `var urlData` and **comment** the urlData with an **ip address** on it.

### Step 13:
**Copy** the url of your `localhost`
##### Example
` http://localhost/ShuttleOffServiceAjax/Service1.svc;`


### Step 14:
**Paste** the url of your locahost to the `var urlData`
##### Example

```sh
var urlData = http://localhost/ShuttleOffServiceAjax/Service1.svc;
```

### Step 15:
Repeat to **Step 12** and do this in every js file with an ajax. 

**(NOTE: ICOMMENT MO LANG YUNG AKIN WAG MO IREMOVE)**

### Step 16:
## Run mo sabihin mo pag may error!!

